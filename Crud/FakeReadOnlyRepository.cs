using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eLib.Utils;

namespace eLib.Crud
{
    /// <summary>
    /// In-memory read-only fake repository to be used in unit testing.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class FakeReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        private readonly Dictionary<Guid, TEntity> _entities = new Dictionary<Guid, TEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeReadOnlyRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public FakeReadOnlyRepository(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _entities.Add(entity.KeyValue(), entity);
            }
        }

        /// <summary>
        /// Gets the number of entities in the repository.
        /// </summary>
        /// <value>The total number of entities.</value>
        public int Count => _entities.Count;

        /// <summary>
        /// Determines whether the repository contains an entity with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><c>true</c> if the repository contains an entity with the specified id; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid id)
        {
            return _entities.ContainsKey(id);
        }

        /// <summary>
        /// Gets the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity.</returns>
        public TEntity Get(Guid id)
        {
            TEntity entity;
            _entities.TryGetValue(id, out entity);
            return entity;
        }

        /// <summary>
        /// Filters the entities based on the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{E}.</returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Values.AsQueryable().Where(predicate);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of entities.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.Values.ToList();
        }
    }
}
