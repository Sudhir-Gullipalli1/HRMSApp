using HRMSApp.Data.Models;
using System;

namespace HRMSApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private hrmsContext _db = new hrmsContext();

        public IRepository<T> RepositoryFor<T>() where T : class
        {
            return new EFRepository<T>(_db);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
