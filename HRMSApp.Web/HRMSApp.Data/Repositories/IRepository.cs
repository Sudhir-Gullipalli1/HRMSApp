using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<List<TEntity>> GetAllAsync();

        DbSet<TEntity> GetDbSet();

        Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, List<string> include = null);

        IQueryable<TEntity> GetBaseQuery();

        Task SaveAsync();

        //Task<string> ExecWithStoreProcedure(string sql, params object[] parameters);
    }
}
