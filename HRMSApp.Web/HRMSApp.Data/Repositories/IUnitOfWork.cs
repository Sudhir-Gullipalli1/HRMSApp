namespace HRMSApp.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> RepositoryFor<T>() where T : class;
    }
}
