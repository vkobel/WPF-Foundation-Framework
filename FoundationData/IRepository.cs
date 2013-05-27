using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoundationData.GenericRepo {

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

      void Create(T entity);
      void Add(T entity);
      void Delete(T entity);

      /// <summary>
      /// Save all the changes into the database
      /// </summary>
      int Persist();
      
      Task<int> AsyncPersist();

   }
}
