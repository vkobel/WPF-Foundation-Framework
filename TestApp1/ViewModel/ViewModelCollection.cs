using KobiDataFramework.GenericRepo;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Data;
using KobiWPFFramework.Ninject;

namespace KobiWPFFramework.ViewModel {

   /// <summary>
   /// Represents a collection of typed ViewModels (all the same type). It exposes a CollectionView property containing
   /// all the ViewModels in a ICollectionView.
   /// </summary>
   /// <typeparam name="TEntity">The entity to be used along with the IRepository interface</typeparam>
   /// <typeparam name="TViewModel">The ViewModel of type DynamicViewModel<TEntity></typeparam>
   public abstract class ViewModelCollection<TEntity, TViewModel> : ViewModelBase where TEntity : class 
                                                                                  where TViewModel : DynamicViewModel<TEntity> {
   
      protected ObservableCollection<TViewModel> all { get; set; }

      /// <summary>
      /// Exposes all the entites as a ICollectionView
      /// </summary>
      public ICollectionView CollectionView { get; set; }

      private IRepository<TEntity> repo { get; set; }

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
         CollectionView = CollectionViewSource.GetDefaultView(all);
         repo = Nj.I.Get<IRepository<TEntity>>();
         var query = predicate != null ? repo.Query(predicate) : repo.GetAllAsEnumerable();
         foreach(var ent in repo.Query(predicate))
            all.Add(Activator.CreateInstance(typeof(TViewModel), ent) as TViewModel);
      }
   
   }
}
