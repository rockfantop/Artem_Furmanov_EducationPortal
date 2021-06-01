using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories
{
    public class Repository<TEntity> : IAsyncRepository<TEntity>
         where TEntity : DbEntity
    {
        private readonly IJsonHandler<TEntity> jsonHandler;

        public Repository(IJsonHandler<TEntity> handler)
        {
            this.jsonHandler = handler;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await this.jsonHandler.CreateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await this.jsonHandler.DeleteAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await this.jsonHandler.UpdateAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(Func<TEntity, bool> condition)
        {
            return await this.jsonHandler.GetAllEntitiesAsync(condition);
        }

        public async Task<TEntity> GetEntityAsync(Func<TEntity, bool> condition)
        {
            return await this.jsonHandler.GetEntityAsync(condition);
        }
    }
}
