namespace Groundwork.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IReadOnlyRepository<TEntity, in TopLevelDomain> 
        where TEntity:IEntity<TopLevelDomain>
    {
        int Count { get; }

        bool Contains(TopLevelDomain id);

        TEntity Get(TopLevelDomain id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Get();
    }
}
