using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Novia.Guestbook.Domain.Abstractions;
using Novia.Guestbook.Domain.Entities;
namespace Novia.Guestbook.Infrastructure.Data.Ef
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronouswrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<TDbContext, TEntity> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : EfDbContext
    {
        protected readonly TDbContext mDbContext;
        public EfRepository(TDbContext dbContext)
        {
            mDbContext = dbContext;
        }
        public virtual TEntity GetById(int id)
        {
            return mDbContext.Set<TEntity>().Find(id);
        }
        public TEntity GetSingleBySpec(ISpecification<TEntity> spec)
        {
            return List(spec).FirstOrDefault();
        }
        public IEnumerable<TEntity> ListAll()
        {
            return mDbContext.Set<TEntity>().AsEnumerable();
        }
        public IEnumerable<TEntity> List(ISpecification<TEntity> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes.Aggregate(mDbContext.Set<TEntity>().AsQueryable(),(current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes,(current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria).AsEnumerable();
        }
        public TEntity Add(TEntity entity)
        {
            mDbContext.Set<TEntity>().Add(entity);
            mDbContext.SaveChanges();
            return entity;
        }
        public void Update(TEntity entity)
        {
            mDbContext.Entry(entity).State = EntityState.Modified;
            mDbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            mDbContext.Set<TEntity>().Remove(entity);
            mDbContext.SaveChanges();
        }
    }
}