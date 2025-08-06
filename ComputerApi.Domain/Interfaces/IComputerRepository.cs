using ComputerApi.Domain.Entities;

namespace ComputerApi.Domain.Interfaces
{
    public interface IComputerRepository
    {
        Task<Computer?> GetByIdAsync(int id);
        Task<Computer?> GetByIdWithSoftwareAsync(int id);
        Task<IEnumerable<Computer>> GetAllAsync();
        Task<Computer> CreateAsync(Computer computer);
        Task<Computer> UpdateAsync(Computer computer);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
