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
    public class EmployeeService : IEmployeeService
    {
        protected IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<Employee>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<Employee>().GetAllAsync();
        }

        async public Task AddEmployee(Employee newEmployee)
        {
            await _unitOfWork.RepositoryFor<Employee>().AddAsync(newEmployee);
            await _unitOfWork.RepositoryFor<Employee>().SaveAsync();
        }

        async public Task UpdateEmployee(Employee employee)
        {
            _unitOfWork.RepositoryFor<Employee>().Update(employee);
            await _unitOfWork.RepositoryFor<Employee>().SaveAsync();
        }

        async public Task DeleteEmployee(Employee employee)
        {
            _unitOfWork.RepositoryFor<Employee>().Delete(employee);
            await _unitOfWork.RepositoryFor<Employee>().SaveAsync();
        }

        async public Task<List<Employee>> FindEmployee(Expression<Func<Employee, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<Employee>().GetBaseQuery();
            return 
            await query
            .Where(expression).ToListAsync();
        }
    }
}