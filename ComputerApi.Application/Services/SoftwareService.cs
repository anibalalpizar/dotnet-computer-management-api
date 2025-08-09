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
    public class SoftwareService : ISoftwareService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SoftwareService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SoftwareDto>>> GetAllSoftwareAsync()
        {
            try
            {
                var software = await _unitOfWork.Software.GetAllAsync();
                var softwareDtos = _mapper.Map<IEnumerable<SoftwareDto>>(software);
                return Result<IEnumerable<SoftwareDto>>.Success(softwareDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SoftwareDto>>.Failure($"Error retrieving software: {ex.Message}");
            }
        }

        public async Task<Result<SoftwareDto>> GetSoftwareByIdAsync(int id)
        {
            try
            {
                var software = await _unitOfWork.Software.GetByIdAsync(id);
                if (software == null)
                    return Result<SoftwareDto>.Failure("Software not found");

                var softwareDto = _mapper.Map<SoftwareDto>(software);
                return Result<SoftwareDto>.Success(softwareDto);
            }
            catch (Exception ex)
            {
                return Result<SoftwareDto>.Failure($"Error retrieving software: {ex.Message}");
            }
        }

        public async Task<Result<SoftwareDto>> CreateSoftwareAsync(CreateSoftwareDto softwareDto)
        {
            try
            {
                var software = _mapper.Map<Software>(softwareDto);
                var createdSoftware = await _unitOfWork.Software.CreateAsync(software);
                await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<SoftwareDto>(createdSoftware);
                return Result<SoftwareDto>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<SoftwareDto>.Failure($"Error creating software: {ex.Message}");
            }
        }

        public async Task<Result<SoftwareDto>> UpdateSoftwareAsync(int id, UpdateSoftwareDto softwareDto)
        {
            try
            {
                var existingSoftware = await _unitOfWork.Software.GetByIdAsync(id);
                if (existingSoftware == null)
                    return Result<SoftwareDto>.Failure("Software not found");

                _mapper.Map(softwareDto, existingSoftware);
                var updatedSoftware = await _unitOfWork.Software.UpdateAsync(existingSoftware);
                await _unitOfWork.SaveChangesAsync();

                var result = _mapper.Map<SoftwareDto>(updatedSoftware);
                return Result<SoftwareDto>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<SoftwareDto>.Failure($"Error updating software: {ex.Message}");
            }
        }

        public async Task<Result> DeleteSoftwareAsync(int id)
        {
            try
            {
                var exists = await _unitOfWork.Software.ExistsAsync(id);
                if (!exists)
                    return Result.Failure("Software not found");

                await _unitOfWork.Software.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting software: {ex.Message}");
            }
        }
    }
}
