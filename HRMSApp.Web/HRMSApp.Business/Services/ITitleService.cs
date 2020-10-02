using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp.Business.Services
{
    public interface ITitleService
    {
        Task<List<Title>> GetAll();

        Task AddTitle(Title newTitle);

        Task UpdateTitle(Title Title);

        Task DeleteTitle(Title title);

        Task<List<Title>> FindTitle(Expression<Func<Title, bool>> expression);
    }
}