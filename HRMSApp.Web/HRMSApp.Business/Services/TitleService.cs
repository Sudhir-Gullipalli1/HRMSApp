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
    public class TitleService : ITitleService
    {
        protected IUnitOfWork _unitOfWork;
        public TitleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async public Task<List<Title>> GetAll()
        {
            return await _unitOfWork.RepositoryFor<Title>().GetAllAsync();
        }

        async public Task AddTitle(Title newTitle)
        {
            await _unitOfWork.RepositoryFor<Title>().AddAsync(newTitle);
            await _unitOfWork.RepositoryFor<Title>().SaveAsync();
        }

        async public Task UpdateTitle(Title title)
        {
            _unitOfWork.RepositoryFor<Title>().Update(title);
            await _unitOfWork.RepositoryFor<Title>().SaveAsync();
        }

        async public Task DeleteTitle(Title title)
        {
            _unitOfWork.RepositoryFor<Title>().Delete(title);
            await _unitOfWork.RepositoryFor<Title>().SaveAsync();
        }

        async public Task<List<Title>> FindTitle(Expression<Func<Title, bool>> expression)
        {
            var query = _unitOfWork.RepositoryFor<Title>().GetBaseQuery();
            return 
            await query
            .Where(expression).ToListAsync();
        }
    }
}