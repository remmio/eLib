using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eLib.Entity;
using eLib.Exceptions;
using eLib.Utils;

#pragma warning disable 1998

namespace eLib.Crud
{
    /// <summary>
    /// In-memory fake repository to be used in unit testing.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region EVENTS

        public event EventHandler<EventArgs<TEntity>> Added;
        public event EventHandler<CancelEventArgs<TEntity>> Adding;
        public event EventHandler<EventArgs<object>> Deleted;
        public event EventHandler<CancelEventArgs<object>> Deleting;
        public event EventHandler<EventArgs<TEntity>> Updated;
        public event EventHandler<CancelEventArgs<TEntity>> Updating;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRepository{TEntity}"/> class.
        /// </summary>
        public FakeRepository()
        {
            Entities = new Dictionary<Guid, TEntity>();
        }

        /// <summary>
        /// Gets the number of entities in the repository.
        /// </summary>
        /// <value>The total number of entities.</value>
        public int Count => Entities.Count;

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        protected Dictionary<Guid, TEntity> Entities { get; }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Operation> Add(TEntity entity)
        {
            Entities[entity.KeyValue()] = entity;
            return Operation.Succes();
        }

        /// <summary>
        /// Determines whether the repository contains an entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns><c>true</c> if the repository contains an entity with the specified id; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid id) => Entities.ContainsKey(id);

        /// <summary>
        /// Gets the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity.</returns>
        public async Task<TEntity> ByGuid(Guid id)
        {
            TEntity entity;
            Entities.TryGetValue(id, out entity);
            return entity;
        }

        /// <summary>
        /// Filters the entities based on the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{E}.</returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => Entities.Values.AsQueryable().Where(predicate);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAll() => Entities.Values.ToList();

        /// <summary>
        /// Removes the entity with the specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        public async Task<Operation> Delete(Guid id) => Entities.Remove(id) ? Operation.Succes() : Operation.Failed();

        /// <summary>
        /// Updates the specified updated entity.
        /// </summary>
        /// <param name="updatedEntity">The updated entity.</param>
        public async Task<Operation> Update(TEntity updatedEntity)
        {
            Entities[updatedEntity.KeyValue()] = updatedEntity;
            return Operation.Succes();
        }
    }
}
