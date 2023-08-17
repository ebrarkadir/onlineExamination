using Microsoft.EntityFrameworkCore;
using OnlineExamination.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.DataAccess.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        internal DbSet<T> dbset;
        private readonly onlineExamDbContext _context = null;

        public GenericRepository(DbSet<T> dbset, onlineExamDbContext context)
        {
            this.dbset = dbset;
            _context = context;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Expression<Func<T>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
