using ComputerApi.Application.Common;
using ComputerApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Application.Services
{
    public interface IComputerService
    {
        Task<Result<IEnumerable<ComputerDto>>> GetAllComputersAsync();
        Task<Result<ComputerDto>> GetComputerByIdAsync(int id);
        Task<Result<ComputerDto>> CreateComputerAsync(CreateComputerDto computerDto);
        Task<Result<ComputerDto>> UpdateComputerAsync(int id, UpdateComputerDto computerDto);
        Task<Result> DeleteComputerAsync(int id);
        Task<Result<IEnumerable<SoftwareDto>>> GetComputerSoftwareAsync(int computerId);
        Task<Result> AddSoftwareToComputerAsync(int computerId, int softwareId);
        Task<Result> RemoveSoftwareFromComputerAsync(int computerId, int softwareId);
    }
}
