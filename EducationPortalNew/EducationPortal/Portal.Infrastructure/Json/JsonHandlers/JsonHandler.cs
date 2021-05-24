using Newtonsoft.Json;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Portal.Infrastructure.Json.JsonHandlers
{
    public class JsonHandler<TEntity> : IJsonHandler<TEntity>
        where TEntity : DbEntity
    {
        public void Create(TEntity entity)
        {
            try
            {
                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                if (userList == null)
                {
                    userList = new List<TEntity>();
                }

                userList.Add(entity);

                using (StreamWriter file = File.CreateText($"..//..//..//..//{typeof(TEntity).Name}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.NullValueHandling = NullValueHandling.Include;
                    serializer.Serialize(file, userList);
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
                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                userList.RemoveAll(x => x.Id == entity.Id);

                userList.Add(entity);

                using (StreamWriter file = File.CreateText($"..//..//..//..//{typeof(TEntity).Name}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.NullValueHandling = NullValueHandling.Include;
                    serializer.Serialize(file, userList);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                List<TEntity> userList = (List<TEntity>)GetAllEntities(x => x == x);

                userList.RemoveAll(x => x.Id == entity.Id);

                using (StreamWriter file = File.CreateText($"..//..//..//..//{typeof(TEntity).Name}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.NullValueHandling = NullValueHandling.Include;
                    serializer.Serialize(file, userList);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public IEnumerable<TEntity> GetAllEntities(Func<TEntity, bool> condition)
        {
            try
            {
                using (StreamReader file = File.OpenText($"..//..//..//..//{typeof(TEntity).Name}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var list = (List<TEntity>)serializer.Deserialize(file, typeof(List<TEntity>));

                    return list.Where(condition).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TEntity GetEntity(Func<TEntity, bool> condition)
        {
            try
            {
                var list = (List<TEntity>)GetAllEntities(condition);

                return list[0];
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
