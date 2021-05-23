﻿using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Interfaces
{
    public interface IXmlHandler<TEntity>
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> GetAllEntities(Func<TEntity, bool> condition);

        TEntity GetEntity(Func<TEntity, bool> condition);

        void Save();
    }
}
