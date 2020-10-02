using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAll();

        Task AddDepartment(Department newDepartment);

        Task UpdateDepartment(Department department);

        Task DeleteDepartment(Department department);

        Task<List<Department>> FindDepartment(Expression<Func<Department, bool>> expression);
    }
}