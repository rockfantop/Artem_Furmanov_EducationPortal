using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portal.Domain.Models;
using Portal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Json.JsonHandlers
{
    public class JsonHandler<TEntity> : IJsonHandler<TEntity>
        where TEntity : DbEntity
    {
        private readonly ILogger<JsonHandler<TEntity>> logger;

        public JsonHandler(ILogger<JsonHandler<TEntity>> logger)
        {
            this.logger = logger;
        }

        public async Task CreateAsync(TEntity entity)
        {
            var enumeration = await GetAllEntitiesAsync(x => x == x);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    List<TEntity> userList = (List<TEntity>)enumeration;

                    if (userList == null || userList.Count == 0)
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
                catch (Exception)
                {
                    this.logger.LogError($"Exception after JsonSerialize", null);
                    return;
                }
            });
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var enumeration = await GetAllEntitiesAsync(x => x == x);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var userList = (List<TEntity>)enumeration;

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
                    this.logger.LogError($"Exception after JsonSerialize", null);
                    return;
                }
            });
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var enumeration = await GetAllEntitiesAsync(x => x == x);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var userList = (List<TEntity>)enumeration;

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
                    this.logger.LogError($"Exception after JsonSerialize", null);
                    return;
                }
            });
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(Func<TEntity, bool> condition)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    using (StreamReader file = File.OpenText($"..//..//..//..//{typeof(TEntity).Name}.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        var list = (List<TEntity>)serializer.Deserialize(file, typeof(List<TEntity>));

                        if (list == null || list.Count == 0)
                        {
                            return null;
                        }

                        return list.Where(condition).ToList();
                    }
                }
                catch (Exception)
                {
                    this.logger.LogError($"Exception after JsonDeserialize", null);
                    return null;
                }
            });
        }

        public async Task<TEntity> GetEntityAsync(Func<TEntity, bool> condition)
        {
            var enumeration = await GetAllEntitiesAsync(condition);

            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = (List<TEntity>)enumeration;

                    if (result == null || result.Count == 0)
                    {
                        return null;
                    }

                    return result[0];
                }
                catch (Exception)
                {
                    this.logger.LogError($"Something happend with data. Elements: {enumeration.Count()}", enumeration);
                    return null;
                }
            });
        }
    }
}
