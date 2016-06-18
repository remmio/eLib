using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eLib.Crud
{
    /// <summary>
    /// Abstract base class for a read only service.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class BaseReadOnlyService<TEntity> where TEntity : IEntity
    {
        private readonly IReadOnlyRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReadOnlyService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if repository is null.</exception>
        protected BaseReadOnlyService(IReadOnlyRepository<TEntity> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        /// <summary>
        /// Count of entities.
        /// </summary>
        /// <returns>The total number of entities.</returns>
        public virtual int Count()
        {
            return _repository.Count;
        }

        /// <summary>
        /// Checks whether an entity with the specified unique identifier exists.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns><c>true</c> if entity exists, <c>false</c> otherwise.</returns>
        public virtual bool Exists(Guid id)
        {
            return _repository.Contains(id);
        }

        /// <summary>
        /// Gets the entity with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns>The entity.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if an entity with the given id is not found.</exception>
        public virtual TEntity Get(Guid id)
        {
            if (_repository.Contains(id))
            {
                return _repository.Get(id);
            }

            throw new KeyNotFoundException(string.Format("{0} with id {1} was not found", typeof(TEntity), id));
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>IEnumerable of entities.</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Filters the entities based on the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{TEntity}.</returns>
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }
    }
}
