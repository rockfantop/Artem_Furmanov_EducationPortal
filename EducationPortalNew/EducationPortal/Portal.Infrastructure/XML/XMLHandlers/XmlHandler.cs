using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Portal.Infrastructure.XML.XMLHandlers
{
    public class XmlHandler<TEntity> : IXmlHandler<TEntity>
        where TEntity : DbEntity
    {
        public void Create(TEntity entity)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<TEntity>));

                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                userList.Add(entity);

                using (var fileStreamForSeriallize = new FileStream($"..//..//..//..//{typeof(TEntity).Name}.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fileStreamForSeriallize, userList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<TEntity>));

                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                userList.RemoveAll(x => x.Id == entity.Id);

                userList.Add(entity);

                using (var fileStreamForSeriallize = new FileStream($"..//..//..//..//{typeof(TEntity).Name}.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fileStreamForSeriallize, userList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<TEntity>));

                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                userList.RemoveAll(x => x.Id == entity.Id);

                using (var fileStreamForSeriallize = new FileStream($"..//..//..//..//{typeof(TEntity).Name}.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fileStreamForSeriallize, userList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<TEntity> GetAllEntities(Func<TEntity, bool> condition)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<TEntity>));

                using (var fileStreamForSeriallize = new FileStream($"..//..//..//..//{typeof(TEntity).Name}.xml", FileMode.Open))
                {
                    var list = (List<TEntity>)formatter.Deserialize(fileStreamForSeriallize);

                    return list.Where(condition).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TEntity GetEntity(Func<TEntity, bool> condition)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(List<TEntity>));

                using (var fileStreamForSeriallize = new FileStream($"..//..//..//..//{typeof(TEntity).Name}.xml", FileMode.Open))
                {
                    var list = (List<TEntity>)formatter.Deserialize(fileStreamForSeriallize);

                    return list.Where(condition).ToList()[0];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
