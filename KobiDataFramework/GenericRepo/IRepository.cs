using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KobiDataFramework.GenericRepo {

   public interface IRepository<T> : IDisposable where T : class {

      T[] GetAll();
      IEnumerable<T> GetAllAsEnumerable();
      IQueryable<T> Query(Expression<Func<T, bool>> predicate);
      T GetSingle(Expression<Func<T, bool>> predicate);

      void Create(T entity);
      void Add(T entity);
      void Delete(T entity);
      int Persist();
      Task<int> AsyncPersist();

   }
}
