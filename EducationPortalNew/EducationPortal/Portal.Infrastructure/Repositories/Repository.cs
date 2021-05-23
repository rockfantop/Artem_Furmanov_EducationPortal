using Portal.Domain.Interfaces;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using Portal.Infrastructure.XML;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : DbEntity
    {
        private readonly IXmlHandler<TEntity> xmlHandler;

        public Repository(IXmlHandler<TEntity> handler)
        {
            this.xmlHandler = handler;
        }

        public void Create(TEntity entity)
        {
            this.xmlHandler.Create(entity);
        }

        public void Delete(TEntity entity)
        {
            this.xmlHandler.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            this.xmlHandler.Update(entity);
        }

        public IEnumerable<TEntity> GetAllEntities(Func<TEntity, bool> condition)
        {
            return this.xmlHandler.GetAllEntities(condition);
        }

        public TEntity GetEntity(Func<TEntity, bool> condition)
        {
            return this.xmlHandler.GetEntity(condition);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
