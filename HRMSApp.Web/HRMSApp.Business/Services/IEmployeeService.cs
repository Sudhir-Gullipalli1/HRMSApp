using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();

        Task AddEmployee(Employee newEmployee);

        Task UpdateEmployee(Employee employee);

        Task DeleteEmployee(Employee employee);

        Task<List<Employee>> FindEmployee(Expression<Func<Employee, bool>> expression);
    }
}