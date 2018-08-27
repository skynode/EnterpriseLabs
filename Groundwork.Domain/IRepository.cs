namespace Groundwork.Domain
{
    public interface IRepository<TEntity, in TopLevelDomain> : IReadOnlyRepository<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        void Add(TEntity entity);

        void Remove(TopLevelDomain id);

        void Update(TEntity entity);
    }
}
