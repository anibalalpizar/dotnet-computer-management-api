using ComputerApi.Application.Common;
using ComputerApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Application.Services
{
    public interface ISoftwareService
    {
        Task<Result<IEnumerable<SoftwareDto>>> GetAllSoftwareAsync();
        Task<Result<SoftwareDto>> GetSoftwareByIdAsync(int id);
        Task<Result<SoftwareDto>> CreateSoftwareAsync(CreateSoftwareDto softwareDto);
        Task<Result<SoftwareDto>> UpdateSoftwareAsync(int id, UpdateSoftwareDto softwareDto);
        Task<Result> DeleteSoftwareAsync(int id);
    }
}
