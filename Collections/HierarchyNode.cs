using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eLib.Collections
{
    // Stefan Cruysberghs, http://www.scip.be, March 2008


    /// <summary>
    /// Hierarchy node class which contains a nested collection of hierarchy nodes
    /// var tree = context.Employees.ToList().AsHierarchy(e => e.EmployeeID, e => e.ReportsTo, 5);
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class HierarchyNode<T> where T : class
    {
        public T Entity { get; set; }
        public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
        public int Depth { get; set; }
        public T Parent { get; set; }
    }

    // Stefan Cruysberghs, July 2008, http://www.scip.be
    /// <summary>
    /// AsHierarchy extension methods for LINQ to SQL IQueryable
    /// </summary>
    public static class LinqToSqlExtensionMethods
    {
        private static IEnumerable<HierarchyNode<TEntity>>
          CreateHierarchy<TEntity>(IQueryable<TEntity> allItems,
            TEntity parentItem,
            string propertyNameId,
            string propertyNameParentId,
            object rootItemId,
            int maxDepth,
            int depth) where TEntity : class
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "e");
            Expression<Func<TEntity, bool>> predicate;

            if (rootItemId != null)
            {
                Expression left = Expression.Property(parameter, propertyNameId);
                left = Expression.Convert(left, rootItemId.GetType());
                Expression right = Expression.Constant(rootItemId);

                predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
            }
            else
            {
                if (parentItem == null)
                {
                    predicate =
                      Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal(Expression.Property(parameter, propertyNameParentId),
                                         Expression.Constant(null)), parameter);
                }
                else
                {
                    Expression left = Expression.Property(parameter, propertyNameParentId);
                    left = Expression.Convert(left, parentItem.GetType().GetProperty(propertyNameId).PropertyType);
                    Expression right = Expression.Constant(parentItem.GetType().GetProperty(propertyNameId).GetValue(parentItem, null));

                    predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
                }
            }

            IEnumerable<TEntity> childs = allItems.Where(predicate).ToList();

            if (!childs.Any()) yield break;
            depth++;

            if ((depth > maxDepth) && (maxDepth != 0)) yield break;
            foreach (var item in childs)
                yield return
                    new HierarchyNode<TEntity>
                    {
                        Entity = item,
                        ChildNodes =
                            CreateHierarchy(allItems, item, propertyNameId, propertyNameParentId, null, maxDepth, depth),
                        Depth = depth,
                        Parent = parentItem
                    };
        }

        /// <summary>
        /// LINQ to SQL (IQueryable) AsHierachy() extension method
        /// var tree = context.Employees.ToList().AsHierarchy(e => e.EmployeeID, e => e.ReportsTo, 5);
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="propertyNameId">String with property name of Id/Key</param>
        /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
          this IQueryable<TEntity> allItems,
          string propertyNameId,
          string propertyNameParentId) where TEntity : class => CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, null, 0, 0);

        /// <summary>
        /// LINQ to SQL (IQueryable) AsHierachy() extension method
        /// var tree = context.Employees.ToList().AsHierarchy(e => e.EmployeeID, e => e.ReportsTo, 5);
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="propertyNameId">String with property name of Id/Key</param>
        /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
        /// <param name="rootItemId">Value of root item Id/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
          this IQueryable<TEntity> allItems,
          string propertyNameId,
          string propertyNameParentId,
          object rootItemId) where TEntity : class => CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, rootItemId, 0, 0);

        /// <summary>
        /// LINQ to SQL (IQueryable) AsHierachy() extension method
        /// var tree = context.Employees.ToList().AsHierarchy(e => e.EmployeeID, e => e.ReportsTo, 5);
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="propertyNameId">String with property name of Id/Key</param>
        /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
        /// <param name="rootItemId">Value of root item Id/Key</param>
        /// <param name="maxDepth">Maximum depth of tree</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
          this IQueryable<TEntity> allItems,
          string propertyNameId,
          string propertyNameParentId,
          object rootItemId,
          int maxDepth) where TEntity : class => CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, rootItemId, maxDepth, 0);
    }

}
