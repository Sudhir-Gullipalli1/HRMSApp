using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRMSApp.Data.Repositories;
using HRMSApp.Data.Models;

namespace HRMSApp.Business.Services
{
    public class DeptManagerService : IDeptManagerService
    {
        protected IUnitOfWork _unitOfWork;
        public DeptManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<DeptManager>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<DeptManager>().GetAllAsync();
        }

        async public Task AddDeptManager(DeptManager newDeptManager)
        {
            await _unitOfWork.RepositoryFor<DeptManager>().AddAsync(newDeptManager);
            await _unitOfWork.RepositoryFor<DeptManager>().SaveAsync();
        }

        async public Task UpdateDeptManager(DeptManager deptManager)
        {
            _unitOfWork.RepositoryFor<DeptManager>().Update(deptManager);
            await _unitOfWork.RepositoryFor<DeptManager>().SaveAsync();
        }

        async public Task DeleteDeptManager(DeptManager deptManager)
        {
            _unitOfWork.RepositoryFor<DeptManager>().Delete(deptManager);
            await _unitOfWork.RepositoryFor<DeptManager>().SaveAsync();
        }

        async public Task<List<DeptManager>> FindDeptManager(Expression<Func<DeptManager, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<DeptManager>().GetBaseQuery();
            return 
            await query
            .Where(expression).ToListAsync();
        }
    }
}