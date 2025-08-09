using AutoMapper;
using ComputerApi.Application.Common;
using ComputerApi.Application.DTOs;
using ComputerApi.Domain.Entities;
using ComputerApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Application.Services
{
    public class ComputerService : IComputerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComputerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ComputerDto>>> GetAllComputersAsync()
        {
            try
            {
                var computers = await _unitOfWork.Computers.GetAllAsync();
                var computerDtos = _mapper.Map<IEnumerable<ComputerDto>>(computers);
                return Result<IEnumerable<ComputerDto>>.Success(computerDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ComputerDto>>.Failure($"Error retrieving computers: {ex.Message}");
            }
        }

        public async Task<Result<ComputerDto>> GetComputerByIdAsync(int id)
        {
            try
            {
                var computer = await _unitOfWork.Computers.GetByIdWithSoftwareAsync(id);
                if (computer == null)
                    return Result<ComputerDto>.Failure("Computer not found");

                var computerDto = _mapper.Map<ComputerDto>(computer);
                return Result<ComputerDto>.Success(computerDto);
            }
            catch (Exception ex)
            {
                return Result<ComputerDto>.Failure($"Error retrieving computer: {ex.Message}");
            }
        }

        public async Task<Result<ComputerDto>> CreateComputerAsync(CreateComputerDto computerDto)
        {
            try
            {
                var computer = _mapper.Map<Computer>(computerDto);
                var createdComputer = await _unitOfWork.Computers.CreateAsync(computer);
                await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<ComputerDto>(createdComputer);
                return Result<ComputerDto>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ComputerDto>.Failure($"Error creating computer: {ex.Message}");
            }
        }

        public async Task<Result<ComputerDto>> UpdateComputerAsync(int id, UpdateComputerDto computerDto)
        {
            try
            {
                var existingComputer = await _unitOfWork.Computers.GetByIdAsync(id);
                if (existingComputer == null)
                    return Result<ComputerDto>.Failure("Computer not found");

                _mapper.Map(computerDto, existingComputer);
                var updatedComputer = await _unitOfWork.Computers.UpdateAsync(existingComputer);
                await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<ComputerDto>(updatedComputer);
                return Result<ComputerDto>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ComputerDto>.Failure($"Error updating computer: {ex.Message}");
            }
        }

        public async Task<Result> DeleteComputerAsync(int id)
        {
            try
            {
                var exists = await _unitOfWork.Computers.ExistsAsync(id);
                if (!exists)
                    return Result.Failure("Computer not found");

                await _unitOfWork.Computers.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting computer: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<SoftwareDto>>> GetComputerSoftwareAsync(int computerId)
        {
            try
            {
                var computerExists = await _unitOfWork.Computers.ExistsAsync(computerId);
                if (!computerExists)
                    return Result<IEnumerable<SoftwareDto>>.Failure("Computer not found");

                var software = await _unitOfWork.Software.GetSoftwareByComputerIdAsync(computerId);
                var softwareDtos = _mapper.Map<IEnumerable<SoftwareDto>>(software);

                return Result<IEnumerable<SoftwareDto>>.Success(softwareDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SoftwareDto>>.Failure($"Error retrieving computer software: {ex.Message}");
            }
        }

        public async Task<Result> AddSoftwareToComputerAsync(int computerId, int softwareId)
        {
            try
            {
                var computerExists = await _unitOfWork.Computers.ExistsAsync(computerId);
                if (!computerExists)
                    return Result.Failure("Computer not found");

                var softwareExists = await _unitOfWork.Software.ExistsAsync(softwareId);
                if (!softwareExists)
                    return Result.Failure("Software not found");

                var isAlreadyInstalled = await _unitOfWork.Software.IsSoftwareInstalledOnComputerAsync(computerId, softwareId);
                if (isAlreadyInstalled)
                    return Result.Failure("Software is already installed on this computer");

                await _unitOfWork.Software.AddSoftwareToComputerAsync(computerId, softwareId);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error adding software to computer: {ex.Message}");
            }
        }

        public async Task<Result> RemoveSoftwareFromComputerAsync(int computerId, int softwareId)
        {
            try
            {
                var computerExists = await _unitOfWork.Computers.ExistsAsync(computerId);
                if (!computerExists)
                    return Result.Failure("Computer not found");

                var isInstalled = await _unitOfWork.Software.IsSoftwareInstalledOnComputerAsync(computerId, softwareId);
                if (!isInstalled)
                    return Result.Failure("Software is not installed on this computer");

                await _unitOfWork.Software.RemoveSoftwareFromComputerAsync(computerId, softwareId);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error removing software from computer: {ex.Message}");
            }
        }
    }
}
