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
    public class ComputerRepository : IComputerRepository
    {
        private readonly ComputerDbContext _context;

        public ComputerRepository(ComputerDbContext context)
        {
            _context = context;
        }

        public async Task<Computer?> GetByIdAsync(int id)
        {
            return await _context.Computers.FindAsync(id);
        }

        public async Task<Computer?> GetByIdWithSoftwareAsync(int id)
        {
            return await _context.Computers
                .Include(c => c.InstalledSoftwares)
                .ThenInclude(ins => ins.Software)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Computer>> GetAllAsync()
        {
            return await _context.Computers
                .Include(c => c.InstalledSoftwares)
                .ThenInclude(ins => ins.Software)
                .ToListAsync();
        }

        public async Task<Computer> CreateAsync(Computer computer)
        {
            _context.Computers.Add(computer);
            return computer;
        }

        public async Task<Computer> UpdateAsync(Computer computer)
        {
            _context.Entry(computer).State = EntityState.Modified;
            return computer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
                return false;

            _context.Computers.Remove(computer);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Computers.AnyAsync(c => c.Id == id);
        }
    }
}
