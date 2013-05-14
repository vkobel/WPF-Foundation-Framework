using GalaSoft.MvvmLight;
using KobiDataFramework.GenericRepo;
using KobiWPFFramework.Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Data;

namespace KobiWPFFramework.ViewModel {

   /// <summary>
   /// Represents a collection of typed ViewModels (all the same type). It exposes a CollectionView property containing
   /// all the ViewModels in a ICollectionView.
   /// </summary>
   /// <typeparam name="TEntity">The entity to be used along with the IRepository interface</typeparam>
   /// <typeparam name="TViewModel">The ViewModel of type DynamicViewModel<TEntity></typeparam>
   public abstract class ViewModelCollection<TEntity, TViewModel> : ViewModelBase, IPreLoadable 
                                                                    where TEntity : class 
                                                                    where TViewModel : ViewModelProxy<TEntity> {

      private IEnumerable<TEntity> entites;
      private ICollectionView collectionView;

      private IRepository<TEntity> repo { get; set; }

      protected ObservableCollection<TViewModel> all { get; set; }

      /// <summary>
      /// Exposes all the entites as a ICollectionView, it is a lazy loaded property
      /// </summary>
      public ICollectionView CollectionView {
         get {
            if(collectionView == null) {
               foreach(var ent in entites)
                  all.Add(Activator.CreateInstance(typeof(TViewModel), ent) as TViewModel);
               collectionView = CollectionViewSource.GetDefaultView(all);
               Thread.Sleep(5000);
            }
            return collectionView;
         }
      }

      /// <summary>
      /// Create an instance with every records in the repository (model)
      /// </summary>
      public ViewModelCollection() : this(null) {
      }

      /// <summary>
      /// Create an instance with all the records matching the given predicate
      /// </summary>
      /// <param name="predicate">The predicate to be applied to the model</param>
      public ViewModelCollection(Expression<Func<TEntity, bool>> predicate) {
         all = new ObservableCollection<TViewModel>();
         repo = Nj.I.Get<IRepository<TEntity>>();
         entites = predicate != null ? repo.Query(predicate) : repo.GetAllAsEnumerable();
      }

      /// <summary>
      /// Must be overridden to perform all kind of long loading. It's used to know when to display a loading screen.
      /// </summary>
      public virtual void PreLoad() {
         var c = CollectionView;
      }

      /// <summary>
      /// Determines if the PreLoading is needed. Default is true. Overridding it and set it to false to disable PreLoading
      /// for very short loading tasks.
      /// </summary>
      /// <returns>A boolean, true if the PreLoad is needed, otherwise false</returns>
      public virtual bool IsPreLoadNeeded() {
         return true;
      }
   }
}
