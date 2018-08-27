using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Groundwork.Domain
{
    public class FakeReadOnlyRepository<TEntity, TopLevelDomain> : IReadOnlyRepository<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        private readonly Dictionary<TopLevelDomain, TEntity> _entities;

        public FakeReadOnlyRepository(IEnumerable<TEntity> entities)
        {
            _entities = new Dictionary<TopLevelDomain, TEntity>();
            foreach (var entity in entities)
            {
                _entities.Add(entity.Id, entity);
            }
        }

        public int Count => _entities.Count;

        public bool Contains(TopLevelDomain id) => _entities.ContainsKey(id);

        public TEntity Get(TopLevelDomain id)
        {
            _entities.TryGetValue(id, out TEntity entity);
            return entity;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => _entities.Values.AsQueryable().Where(predicate);

        public IEnumerable<TEntity> Get() => _entities.Values.ToList();
    }
}
