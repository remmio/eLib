using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eLib.Entity;
using eLib.Exceptions;

namespace eLib.Crud
{
    /// <summary>
    /// Generic entity repository abstraction.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity 
    {

        #region CRUD

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<Operation> Add(TEntity entity);

        /// <summary>
        /// Removes the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        Task<Operation> Delete(Guid id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<Operation> Update(TEntity entity);

        /// <summary>
        /// Gets the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity.</returns>
        Task<TEntity> ByGuid(Guid id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>IEnumerable of entities.</returns>
        Task<IEnumerable<TEntity>> GetAll();

        #endregion


        #region EVENTS

        /// <summary>
        /// Occurs when an entity is added.
        /// </summary>
        event EventHandler<EventArgs<TEntity>> Added;

        /// <summary>
        /// Occurs before adding an entity.
        /// </summary>
        event EventHandler<CancelEventArgs<TEntity>> Adding;

        /// <summary>
        /// Occurs when an entity is deleted.
        /// </summary>
        event EventHandler<EventArgs<object>> Deleted;

        /// <summary>
        /// Occurs before deleting an entity.
        /// </summary>
        event EventHandler<CancelEventArgs<object>> Deleting;

        /// <summary>
        /// Occurs when an entity is updated.
        /// </summary>
        event EventHandler<EventArgs<TEntity>> Updated;

        /// <summary>
        /// Occurs before updating an entity.
        /// </summary>
        event EventHandler<CancelEventArgs<TEntity>> Updating;

        #endregion

    }
}
