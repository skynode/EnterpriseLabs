namespace Groundwork.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    public abstract class BaseReadOnlyService<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        private readonly IReadOnlyRepository<TEntity, TopLevelDomain> _repository;

        protected BaseReadOnlyService(IReadOnlyRepository<TEntity, TopLevelDomain> repository)
        {
            _repository = repository ?? throw new ArgumentNullException();
        }

        public virtual bool Exists(TopLevelDomain id) => _repository.Contains(id);

        public virtual TEntity Get(TopLevelDomain id)
        {
            if (_repository.Contains(id))
            {
                return _repository.Get(id);
            }
            throw new KeyNotFoundException($"{typeof(TEntity)} with id {id} was not found");
        }

        public virtual IEnumerable<TEntity> Get() => _repository.Get();

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expr)
            => _repository.Get(expr);
    }
}
