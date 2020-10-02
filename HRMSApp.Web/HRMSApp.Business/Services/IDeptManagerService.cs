using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface IDeptManagerService
    {
        Task<List<DeptManager>> GetAll();

        Task AddDeptManager(DeptManager newDeptManager);

        Task UpdateDeptManager(DeptManager DeptManager);

        Task DeleteDeptManager(DeptManager DeptManager);

        Task<List<DeptManager>> FindDeptManager(Expression<Func<DeptManager, bool>> expression);
    }
}