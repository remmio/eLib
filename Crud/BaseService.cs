using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eLib.Entity;
using eLib.Exceptions;

namespace eLib.Crud
{
    /// <summary>
    /// Abstract base class for a service.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class BaseService<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region EVENTS

        /// <summary>
        /// Occurs when an entity is added.
        /// </summary>
        public event EventHandler<EventArgs<TEntity>> Added;

        /// <summary>
        /// Occurs before adding an entity.
        /// </summary>
        public event EventHandler<CancelEventArgs<TEntity>> Adding;

        /// <summary>
        /// Occurs when an entity is deleted.
        /// </summary>
        public event EventHandler<EventArgs<object>> Deleted;

        /// <summary>
        /// Occurs before deleting an entity.
        /// </summary>
        public event EventHandler<CancelEventArgs<object>> Deleting;

        /// <summary>
        /// Occurs when an entity is updated.
        /// </summary>
        public event EventHandler<EventArgs<TEntity>> Updated;

        /// <summary>
        /// Occurs before updating an entity.
        /// </summary>
        public event EventHandler<CancelEventArgs<TEntity>> Updating;

        #endregion

        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReadOnlyService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if repository is null.</exception>
        protected BaseService(IRepository<TEntity> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }


        #region CRUD


        public virtual async Task<Operation> Add(TEntity entity)
        {
            var cancelEventArgs = new CancelEventArgs<TEntity>(entity);

            OnAdding(cancelEventArgs);
            if (cancelEventArgs.Cancel) Operation.Canceled();

            var result = await _repository.Add(entity);
            if (result == Operation.Succes())
                OnAdded(entity);
            return result;
        }

        public virtual async Task<Operation> Delete(Guid id)
        {
            var cancelEventArgs = new CancelEventArgs<object>(id);
            OnDeleting(cancelEventArgs);
            if (cancelEventArgs.Cancel) Operation.Canceled();

            var result = await _repository.Delete(id);
            if (result == Operation.Succes())
                OnDeleted(id);
            return result;           
        }
      
        public virtual async Task<Operation> Update(TEntity entity)
        {
            var cancelEventArgs = new CancelEventArgs<TEntity>(entity);
            OnUpdating(cancelEventArgs);
            if (cancelEventArgs.Cancel) Operation.Canceled();

            var result = await _repository.Update(entity);
            if (result == Operation.Succes())
                OnUpdated(entity);

            return result;
        }

        public virtual async Task<TEntity> ByGuid(Guid id) => await _repository.ByGuid(id);

        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _repository.GetAll();

        #endregion



        #region ON EVENT

        /// <summary>
        /// Raises the Added event.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnAdded(TEntity entity) => Added?.Invoke(this, new EventArgs<TEntity>(entity));

        /// <summary>
        /// Raises the Adding event.
        /// </summary>
        /// <param name="e">The CancelEventArgs.</param>
        protected virtual void OnAdding(CancelEventArgs<TEntity> e) => Adding?.Invoke(this, e);

        /// <summary>
        /// Raises the Deleted event.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        protected virtual void OnDeleted(Guid entityId) => Deleted?.Invoke(this, new EventArgs<object>(entityId));

        /// <summary>
        /// Raises the Deleting event.
        /// </summary>
        /// <param name="e">The CancelEventArgs.</param>
        protected virtual void OnDeleting(CancelEventArgs<object> e) => Deleting?.Invoke(this, e);

        /// <summary>
        /// Raises the Updated event.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void OnUpdated(TEntity entity) => Updated?.Invoke(this, new EventArgs<TEntity>(entity));

        /// <summary>
        /// Raises the Updating event.
        /// </summary>
        /// <param name="e">The CancelEventArgs.</param>
        protected virtual void OnUpdating(CancelEventArgs<TEntity> e) => Updating?.Invoke(this, e);

        #endregion

       
    }
}
