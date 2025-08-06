using ComputerApi.Domain.Entities;
using ComputerApi.Domain.Interfaces;
using ComputerApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Infrastructure.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly ComputerDbContext _context;

        public SoftwareRepository(ComputerDbContext context)
        {
            _context = context;
        }

        public async Task<Software?> GetByIdAsync(int id)
        {
            return await _context.Software.FindAsync(id);
        }

        public async Task<IEnumerable<Software>> GetAllAsync()
        {
            return await _context.Software.ToListAsync();
        }

        public async Task<Software> CreateAsync(Software software)
        {
            _context.Software.Add(software);
            return software;
        }

        public async Task<Software> UpdateAsync(Software software)
        {
            _context.Entry(software).State = EntityState.Modified;
            return software;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var software = await _context.Software.FindAsync(id);
            if (software == null)
                return false;

            _context.Software.Remove(software);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Software.AnyAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Software>> GetSoftwareByComputerIdAsync(int computerId)
        {
            return await _context.InstalledSoftware
                .Where(ins => ins.ComputerId == computerId)
                .Include(ins => ins.Software)
                .Select(ins => ins.Software)
                .ToListAsync();
        }

        public async Task<bool> AddSoftwareToComputerAsync(int computerId, int softwareId)
        {
            var installedSoftware = new InstalledSoftware
            {
                ComputerId = computerId,
                SoftwareId = softwareId,
                InstallationDate = DateTime.UtcNow
            };

            _context.InstalledSoftware.Add(installedSoftware);
            return true;
        }

        public async Task<bool> RemoveSoftwareFromComputerAsync(int computerId, int softwareId)
        {
            var installedSoftware = await _context.InstalledSoftware
                .FirstOrDefaultAsync(ins => ins.ComputerId == computerId && ins.SoftwareId == softwareId);

            if (installedSoftware == null)
                return false;

            _context.InstalledSoftware.Remove(installedSoftware);
            return true;
        }

        public async Task<bool> IsSoftwareInstalledOnComputerAsync(int computerId, int softwareId)
        {
            return await _context.InstalledSoftware
                .AnyAsync(ins => ins.ComputerId == computerId && ins.SoftwareId == softwareId);
        }
    }
}
