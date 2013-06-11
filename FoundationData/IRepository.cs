using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoundationData.GenericRepo {

   /// <summary>
   /// Specify all the necessary methods of a Repository pattern
   /// </summary>
   /// <typeparam name="T">The type of entity that the repository will hold</typeparam>
   public interface IRepository<T> : IDisposable where T : class {

      /// <summary>
      /// Return an array of all entites (not lazy)
      /// </summary>
      T[] GetAll();

      /// <summary>
      /// Return an array of all entites (lazy)
      /// </summary>
      IEnumerable<T> GetAllAsEnumerable();

      /// <summary>
      /// Return all entites matching the predicate
      /// </summary>
      IQueryable<T> Query(Expression<Func<T, bool>> predicate);

      /// <summary>
      /// Return the single entity matching the predicate
      /// </summary>
      T GetSingle(Expression<Func<T, bool>> predicate);

      /// <summary>
      /// Reloads the entity object form the database and return the specified property
      /// </summary>
      /// <param name="entity">The entity that needs the reload</param>
      /// <param name="property">The property to be returned</param>
      /// <returns>The updated property form the entity</returns>
      object GetReloadedProperty(T entity, string property);

      /// <summary>
      /// Add the specified entity to the set and persist immediately the database
      /// </summary>
      /// <param name="entity">The entity to insert in the database</param>
      void Create(T entity);

      /// <summary>
      /// Add the specified entity to the set without persiting it
      /// </summary>
      /// <param name="entity">The entity to insert in the set</param>
      void Add(T entity);

      /// <summary>
      /// Deletes the specified entity in the set
      /// </summary>
      /// <param name="entity">The entity to delete</param>
      void Delete(T entity);

      /// <summary>
      /// Save all the changes into the database
      /// </summary>
      /// <returns>The number of objects written to the underlying database</returns>
      int Persist();
   }
}
