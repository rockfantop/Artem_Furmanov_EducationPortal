using Portal.Domain.Entities;
using Portal.Domain.Models;
using Portal.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Domain.Interfaces
{
    public interface IEfRepository<TEntity>
        where TEntity : class
    {

        //Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<TEntity> GetEntityAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<PagedList<TEntity>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        Task<PagedList<TEntity>> GetAsync(Specification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetWithInclude(Specification<TEntity> specification, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<PagedList<TEntity>> GetListWithInclude(Specification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        Task SaveChanges();
    }
}
