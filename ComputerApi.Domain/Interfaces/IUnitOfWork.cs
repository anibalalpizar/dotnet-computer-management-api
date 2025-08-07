namespace ComputerApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IComputerRepository Computers { get; }
        ISoftwareRepository Software { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
