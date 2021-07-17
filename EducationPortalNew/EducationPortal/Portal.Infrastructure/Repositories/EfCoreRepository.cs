using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Domain.Specifications;
using Portal.EfCore.Context;
using Portal.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories
{
    public class EfCoreRepository<TEntity> : IEfRepository<TEntity>
        where TEntity : class
    {
        protected readonly PortalDbContext context;
        protected readonly DbSet<TEntity> entities;

        public EfCoreRepository(PortalDbContext context)
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = this.entities.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.entities.AddAsync(entity, cancellationToken).AsTask();
        }

        public async Task AddAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            await this.entities.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<TEntity> FindAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.entities.FirstOrDefaultAsync(specification.Expression, cancellationToken);
        }

        public async Task<TEntity> GetEntityAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.entities.Where(specification.Expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.entities.Where(specification.Expression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<PagedList<TEntity>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await this.entities.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<PagedList<TEntity>> GetAsync(Specification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await this.entities.Where(specification.Expression).ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWithInclude(Specification<TEntity> specification, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.Where(specification.Expression).ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetListWithInclude(Specification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.Where(specification.Expression).ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.entities.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            this.entities.RemoveRange(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.entities.Update(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            this.entities.UpdateRange(entity);
            await Task.CompletedTask;
        }
    }
}
