using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IGenericRepository<TEntity, TContext>
       where TEntity : class
       where TContext : DbContext
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IEnumerable<TEntity> GetAll();

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void DeleteRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate); // Updated for proper async support



        Task AddAsync(TEntity entity);

        Task<OutLetMasterDetail> GetCoordinatesAsync(int? rscode, string PartyName);

    }
}
