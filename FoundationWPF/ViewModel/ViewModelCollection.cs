using FoundationData.GenericRepo;
using FoundationWPF.Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Data;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// Represents a collection of typed ViewModels (all the same type). It exposes a "CollectionView" property containing
   /// all the ViewModels in a ICollectionView.
   /// </summary>
   /// <typeparam name="TEntity">The entity to be used along with the IRepository interface</typeparam>
   /// <typeparam name="TViewModel">The ViewModel of type DynamicViewModel<TEntity></typeparam>
   public abstract class ViewModelCollection<TEntity, TViewModel> : ViewModelFoundation, IPreLoadable 
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
               //Thread.Sleep(5000);
               foreach(var ent in entites) // real loading of entites (previously lazily loaded)
                  all.Add(Activator.CreateInstance(typeof(TViewModel), ent) as TViewModel);
               collectionView = CollectionViewSource.GetDefaultView(all);
            }
            return collectionView;
         }
      }

      /// <summary>
      /// Create a new ViewModelCollection, containing by default every entites. Use the 
      /// Filter(predicate) method to apply a filter.
      /// </summary>
      public ViewModelCollection() : base() {
         all = new ObservableCollection<TViewModel>();
         repo = Nj.I.Get<IRepository<TEntity>>();
         entites = repo.GetAllAsEnumerable(); // lazy loaded
         IsPreLoadNeeded = true;
      }

      /// <summary>
      /// Filter the entites collection with a given predicate, null for all entites.
      /// </summary>
      /// <param name="predicate">The predicate to filter the entites collection, null to select all entites.</param>
      public void Filter(Expression<Func<TEntity, bool>> predicate) {
         entites = predicate != null ? repo.Query(predicate) : repo.GetAllAsEnumerable();
      }

      #region IPreLoadable stuff

      public ViewModelFoundation LoadingViewModel { get; set; }

      /// <summary>
      /// Must be overridden to perform all kind of long loading. It's used to know when to display a loading screen.
      /// </summary>
      public virtual void PreLoad() {
         var c = CollectionView;
      }

      public virtual bool IsPreLoadNeeded { get; set; }

      public virtual bool IsCurrentlyLoading { get; set; }

      #endregion
   }
}
 