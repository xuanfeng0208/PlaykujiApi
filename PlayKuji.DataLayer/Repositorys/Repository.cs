using Microsoft.EntityFrameworkCore;
using PlayKuji.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlayKuji.DataLayer.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext _dbContext { get; set; }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Any(predicate);
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public T FirstOrDefault()
        {
            return _dbContext.Set<T>().FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
    }
}