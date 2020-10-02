using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetAll();

        Task AddSalary(Salary newSalary);

        Task UpdateSalary(Salary Salary);

        Task DeleteSalary(Salary Salary);

        Task<List<Salary>> FindSalary(Expression<Func<Salary, bool>> expression);
    }
}