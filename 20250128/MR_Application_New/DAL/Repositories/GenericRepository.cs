using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
       where TEntity : class
       where TContext : DbContext
    {
        protected readonly TContext context;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(TContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<OutLetMasterDetail> GetCoordinatesAsync(int? rscode, string PartyName)
        {
            var locationQuery = context.Set<OutLetMasterDetail>().AsQueryable();

            if (!string.IsNullOrEmpty(PartyName))
            {
                locationQuery = locationQuery.Where(l => l.Rscode == rscode && l.PartyName == PartyName);
            }
            else if (rscode.HasValue)
            {
                locationQuery = locationQuery.Where(l => l.Rscode == rscode.Value);
            }

            return await locationQuery
                .Select(l => new OutLetMasterDetail { Latitude = l.Latitude, Longitude = l.Longitude })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}
