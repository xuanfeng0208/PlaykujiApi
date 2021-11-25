using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlayKuji.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T: class
    {
        DbContext _dbContext { get; set; }

        bool Any(Expression<Func<T,bool>> predicate);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T FirstOrDefault();

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}