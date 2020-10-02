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
    public class SalaryService : ISalaryService
    {
        protected IUnitOfWork _unitOfWork;
        public SalaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<Salary>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<Salary>().GetAllAsync();
        }

        async public Task AddSalary(Salary newSalary)
        {
            await _unitOfWork.RepositoryFor<Salary>().AddAsync(newSalary);
            await _unitOfWork.RepositoryFor<Salary>().SaveAsync();
        }

        async public Task UpdateSalary(Salary salary)
        {
            _unitOfWork.RepositoryFor<Salary>().Update(salary);
            await _unitOfWork.RepositoryFor<Salary>().SaveAsync();
        }

        async public Task DeleteSalary(Salary salary)
        {
            _unitOfWork.RepositoryFor<Salary>().Delete(salary);
            await _unitOfWork.RepositoryFor<Salary>().SaveAsync();
        }

        async public Task<List<Salary>> FindSalary(Expression<Func<Salary, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<Salary>().GetBaseQuery();
            return 
            await query
            .Where(expression).ToListAsync();
        }
    }
}