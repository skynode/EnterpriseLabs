namespace Groundwork.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    public class FakeRepository<TEntity, TopLevelDomain> : IRepository<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        public FakeRepository()
        {
            Entities = new Dictionary<TopLevelDomain, TEntity>();
        }

        protected Dictionary<TopLevelDomain, TEntity> Entities { get; }

        public int Count => Entities.Count();

        public void Add(TEntity entity)
        {
            Entities[entity.Id] = entity;
        }

        public bool Contains(TopLevelDomain id) => Entities.ContainsKey(id);

        public TEntity Get(TopLevelDomain id)
        {
            Entities.TryGetValue(id, out TEntity entity);
            return entity;
        }

        public IEnumerable<TEntity> Get() => Entities.Values.ToList();

        public void Remove(TopLevelDomain id) => Entities.Remove(id);

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => Entities.Values.AsQueryable().Where(predicate);

        public void Update(TEntity updatedEntity) => Entities[updatedEntity.Id] = updatedEntity;
    }
}
