namespace MVC.Repositories{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}