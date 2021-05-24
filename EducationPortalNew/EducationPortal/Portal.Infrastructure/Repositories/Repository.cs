using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : DbEntity
    {
        private readonly IJsonHandler<TEntity> jsonHandler;

        public Repository(IJsonHandler<TEntity> handler)
        {
            this.jsonHandler = handler;
        }

        public void Create(TEntity entity)
        {
            this.jsonHandler.Create(entity);
        }

        public void Delete(TEntity entity)
        {
            this.jsonHandler.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            this.jsonHandler.Update(entity);
        }

        public IEnumerable<TEntity> GetAllEntities(Func<TEntity, bool> condition)
        {
            return this.jsonHandler.GetAllEntities(condition);
        }

        public TEntity GetEntity(Func<TEntity, bool> condition)
        {
            return this.jsonHandler.GetEntity(condition);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
