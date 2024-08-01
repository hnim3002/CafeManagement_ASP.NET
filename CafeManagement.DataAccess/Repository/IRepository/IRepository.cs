using CafeManagement.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{       
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll(
            string? includeProperties = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<IEnumerable<T>> GetAllAsync(
            string? includeProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<T> AddAsync(T entity);
        void Add(T entity);
        void Delete(T entity);
        Task<T> DeleteAsync(T entity);
        void RemoveRange(IEnumerable<T> entities);
        //void Update(Customer obj);
    }
}
