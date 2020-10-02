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
    public class DepartmentService : IDepartmentService
    {
        protected IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<Department>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<Department>().GetAllAsync();
        }

        async public Task AddDepartment(Department newDepartment)
        {
            await _unitOfWork.RepositoryFor<Department>().AddAsync(newDepartment);
            await _unitOfWork.RepositoryFor<Department>().SaveAsync();
        }

        async public Task UpdateDepartment(Department department)
        {
            _unitOfWork.RepositoryFor<Department>().Update(department);
            await _unitOfWork.RepositoryFor<Department>().SaveAsync();
        }

        async public Task DeleteDepartment(Department department)
        {
            _unitOfWork.RepositoryFor<Department>().Delete(department);
            await _unitOfWork.RepositoryFor<Department>().SaveAsync();
        }

        async public Task<List<Department>> FindDepartment(Expression<Func<Department, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<Department>().GetBaseQuery();
            return
            await query
            .Where(expression).ToListAsync();
        }
    }
}