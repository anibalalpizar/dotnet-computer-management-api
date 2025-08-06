using ComputerApi.Domain.Interfaces;
using ComputerApi.Infrastructure.Data;
using ComputerApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComputerDbContext _context;
        private IDbContextTransaction? _transaction;
        private IComputerRepository? _computers;
        private ISoftwareRepository? _software;

        public UnitOfWork(ComputerDbContext context)
        {
            _context = context;
        }

        public IComputerRepository Computers => _computers ??= new ComputerRepository(_context);
        public ISoftwareRepository Software => _software ??= new SoftwareRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
