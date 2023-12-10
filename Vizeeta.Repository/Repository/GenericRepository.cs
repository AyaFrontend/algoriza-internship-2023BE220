using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Data;
using Vizeeta.Domain.Entities;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.ISpecification;
using Vizeeta.Repository.Specification;

namespace Vizeeta.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).ToListAsync();


        public async Task<T> GetByIdAsync(string id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).CountAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
            => await ApplySpecifications(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificatioEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }


        public void Add(T entity)
           => _context.Set<T>().Add(entity);
        public void Update(T entity)
            => _context.Set<T>().Update(entity);
        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

    }
}
