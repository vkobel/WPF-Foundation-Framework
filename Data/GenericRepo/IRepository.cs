using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Data.GenericRepo {

   public interface IRepository<T> : IDisposable where T : class {

      DbSet<T> Entities { get; }

      T[] GetAll();
      IQueryable<T> Query(Expression<Func<T, bool>> predicate);
      T GetSingle(Expression<Func<T, bool>> predicate);

      void Create(T entity);
      void Add(T entity);
      void Delete(T entity);
      int Persist();

   }
}
