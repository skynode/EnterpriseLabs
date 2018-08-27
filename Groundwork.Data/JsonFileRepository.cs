namespace Groundwork.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Groundwork.Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class JsonFileRepository<TEntity, TopLevelDomain> : IRepository<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        private readonly Dictionary<TopLevelDomain, TEntity> _entities;
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonFileRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _entities = new Dictionary<TopLevelDomain, TEntity>();
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.Converters.Add(new StringEnumConverter());
            _serializerSettings.Formatting = Formatting.Indented;
            _serializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            _serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            _serializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            if (File.Exists(filePath))
            {
                using(var streamReader = new StreamReader(filePath))
                {
                    var json = streamReader.ReadToEnd();
                    _entities = JsonConvert.DeserializeObject<Dictionary<TopLevelDomain, TEntity>>(json, _serializerSettings);
                }
            }
        }
        private void Persist()
        {
            using(var streamWriter = new StreamWriter(_filePath))
            {
                var json = JsonConvert.SerializeObject(_entities, _serializerSettings);
                streamWriter.Write(json);
            }
        }

        public int Count => _entities.Count;

        public void Add(TEntity entity)
        {
            _entities[entity.Id] = entity;
            Persist();
        }

        public void Remove(TopLevelDomain id)
        {
            _entities.Remove(id);
            Persist();
        }

        public void Update(TEntity updatedEntity)
        {
            _entities[updatedEntity.Id] = updatedEntity;
            Persist();
        }

        public bool Contains(TopLevelDomain id) => _entities.ContainsKey(id);

        public TEntity Get(TopLevelDomain id) => _entities[id];

        public virtual IEnumerable<TEntity> Get() => _entities.Values.ToArray();

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expr)
            => _entities.Values.AsQueryable().Where(expr);
    }
}
