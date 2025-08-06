using ComputerApi.Domain.Entities;

namespace ComputerApi.Domain.Interfaces
{
    public interface ISoftwareRepository
    {
        Task<Software?> GetByIdAsync(int id);
        Task<IEnumerable<Software>> GetAllAsync();
        Task<Software> CreateAsync(Software software);
        Task<Software> UpdateAsync(Software software);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Software>> GetSoftwareByComputerIdAsync(int computerId);
        Task<bool> AddSoftwareToComputerAsync(int computerId, int softwareId);
        Task<bool> RemoveSoftwareFromComputerAsync(int computerId, int softwareId);
        Task<bool> IsSoftwareInstalledOnComputerAsync(int computerId, int softwareId);
    }
}
