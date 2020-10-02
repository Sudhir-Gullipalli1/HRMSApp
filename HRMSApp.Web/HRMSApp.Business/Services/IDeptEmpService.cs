using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface IDeptEmpService
    {
        Task<List<DeptEmp>> GetAll();

        Task AddDeptEmp(DeptEmp newDeptEmp);

        Task UpdateDeptEmp(DeptEmp DeptEmp);

        Task DeleteDeptEmp(DeptEmp DeptEmp);

        Task<List<DeptEmp>> FindDeptEmp(Expression<Func<DeptEmp, bool>> expression);
    }
}