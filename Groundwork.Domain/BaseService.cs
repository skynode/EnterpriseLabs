namespace Groundwork.Domain
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseService<TEntity, TopLevelDomain> : BaseReadOnlyService<TEntity, TopLevelDomain>
        where TEntity : IEntity<TopLevelDomain>
    {
        private readonly IRepository<TEntity, TopLevelDomain> _repository;

        protected BaseService(IRepository<TEntity, TopLevelDomain> repository)
            : base(repository) => _repository = repository;

        public event EventHandler<EventArgs<TEntity>> Added;

        public event EventHandler<CancelEventArgs<TEntity>> Adding;

        public event EventHandler<EventArgs<object>> Deleted;

        public event EventHandler<CancelEventArgs<object>> Deleting;

        public event EventHandler<EventArgs<TEntity>> Updated;

        public event EventHandler<CancelEventArgs<TEntity>> Updating;

        protected virtual void OnAdded(TEntity entity)
            => Added?.Invoke(this, new EventArgs<TEntity>(entity));

        protected virtual void OnAdding(CancelEventArgs<TEntity> e)
            => Adding?.Invoke(this, e);

        protected virtual void OnDeleted(object entityId)
            => Deleted?.Invoke(this, new EventArgs<object>(entityId));

        protected virtual void OnDeleting(CancelEventArgs<object> e)
            => Deleting?.Invoke(this, e);

        protected virtual void OnUpdated(TEntity entity)
            => Updated?.Invoke(this, new EventArgs<TEntity>(entity));

        protected virtual void OnUpdating(CancelEventArgs<TEntity> e)
            => Updating?.Invoke(this, e);

        public virtual void Add(TEntity entity)
        {
            if (!_repository.Contains(entity.Id))
            {
                var cancelEventArgs = new CancelEventArgs<TEntity>(entity);
                OnAdding(cancelEventArgs);
                if (!cancelEventArgs.Cancel)
                {
                    _repository.Add(entity);
                    OnAdded(entity);
                }
            }
        }

        public virtual void Remove(TopLevelDomain id)
        {
            if (!_repository.Contains(id))
            {
                var cancelEventArgs = new CancelEventArgs<object>(id);
                OnDeleting(cancelEventArgs);
                if (!cancelEventArgs.Cancel)
                {
                    _repository.Remove(id);
                    OnDeleted(id);
                }
            }
            else
            {
                throw new KeyNotFoundException($"{typeof(TEntity)} with id {id} was not found.");
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (_repository.Contains(entity.Id))
            {
                var cancelEventArgs = new CancelEventArgs<TEntity>(entity);
                OnUpdating(cancelEventArgs);
                if (!cancelEventArgs.Cancel)
                {
                    _repository.Update(entity);
                    OnUpdated(entity);
                }
            }
            else
            {
                throw new KeyNotFoundException($"{typeof(TEntity)} with id {entity.Id} was not found.");
            }
        }
        
    }
}
