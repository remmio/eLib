using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eLib.Crud
{
    /// <summary>
    /// Generic read-only entity repository abstraction.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    // <typeparam name="TId">The entity ID type.</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Gets the number of entities in the repository.
        /// </summary>
        /// <returns>The total number of entities.</returns>
        int Count { get; }

        /// <summary>
        /// Determines whether the repository contains an entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns><c>true</c> if the repository contains an entity with the specified id; otherwise, <c>false</c>.</returns>
        bool Contains(Guid id);

        /// <summary>
        /// Gets the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity.</returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Filters the entities based on the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{TEntity}.</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of entities.</returns>
        IEnumerable<TEntity> GetAll();
    }
}
