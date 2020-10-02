using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Data.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _db;
        private DbSet<TEntity> _dbSet;

        public EFRepository(DbContext context)
        {
            this._db = context;
            this._dbSet = context.Set<TEntity>();
        }

        async public virtual Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        async public Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync<TEntity>();
        }

        public DbSet<TEntity> GetDbSet()
        {
            return _dbSet;
        }

        async public Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, List<string> includes)
        {
            var query = this._db.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = await query.Where(expression).ToListAsync();
            return result;
        }

        public IQueryable<TEntity> GetBaseQuery()
        {
            var query = this._db.Set<TEntity>().AsQueryable();
            return query;
        }

        async public Task SaveAsync()
        {
            await this._db.SaveChangesAsync().ConfigureAwait(false);
        }

        //async public Task<string> ExecWithStoreProcedure(string sql, params object[] parameters)
        //{
        //    var result = await this._db.Database.ExecuteSqlCommandAsync(sql, parameters);
        //    return ((SqlParameter)parameters[2]).Value.ToString();
        //}

    }
}
