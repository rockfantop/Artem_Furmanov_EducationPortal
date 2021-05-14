using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Interfaces
{
    public interface IRepository <T>
        where T : class
    {
        IEnumerable<T> GetAllEntities();

        T GetEntity(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        void Save();
    }
}
