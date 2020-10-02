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
    public class DeptEmpService : IDeptEmpService
    {
        protected IUnitOfWork _unitOfWork;
        public DeptEmpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<DeptEmp>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<DeptEmp>().GetAllAsync();
        }

        async public Task AddDeptEmp(DeptEmp newDeptEmp)
        {
            await _unitOfWork.RepositoryFor<DeptEmp>().AddAsync(newDeptEmp);
            await _unitOfWork.RepositoryFor<DeptEmp>().SaveAsync();
        }

        async public Task UpdateDeptEmp(DeptEmp deptEmp)
        {
            _unitOfWork.RepositoryFor<DeptEmp>().Update(deptEmp);
            await _unitOfWork.RepositoryFor<DeptEmp>().SaveAsync();
        }

        async public Task DeleteDeptEmp(DeptEmp deptEmp)
        {
            _unitOfWork.RepositoryFor<DeptEmp>().Delete(deptEmp);
            await _unitOfWork.RepositoryFor<DeptEmp>().SaveAsync();
        }

        async public Task<List<DeptEmp>> FindDeptEmp(Expression<Func<DeptEmp, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<DeptEmp>().GetBaseQuery();
            return 
            await query
            .Where(expression).ToListAsync();
        }
    }
}