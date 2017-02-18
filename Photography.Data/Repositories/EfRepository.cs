namespace Photography.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Interfaces;

    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> set;

        public EfRepository(DbSet<TEntity> set)
        {
            this.set = set;
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.set;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> @where)
        {
            return this.set.Where(@where);
        }

        public void Add(TEntity entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.set.AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            this.set.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this.set.RemoveRange(entities);
        }

        public TEntity FindById(int id)
        {
            return this.set.Find(id);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> @where)
        {
            return this.set.Where(@where);
        }
    }
}