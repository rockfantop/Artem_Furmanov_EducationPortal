using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Interfaces
{
    public interface IAsyncRepository <TEntity>
        where TEntity : DbEntity
    {
        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllEntitiesAsync(Func<TEntity, bool> condition);

        Task<TEntity> GetEntityAsync(Func<TEntity, bool> condition);
    }
}
