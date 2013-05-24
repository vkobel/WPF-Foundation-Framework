using FoundationData.GenericRepo;
using FoundationWPF.DI;
using System.ComponentModel;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// Represents a generic ViewModel for a specific model entity. Often, this is the object exposed to the view
   /// directly, or via the ViewModelCollection.
   /// </summary>
   /// <typeparam name="TEntity">The type of the model entity (to create the repository)</typeparam>
   public abstract class ViewModelProxy<TEntity> : ViewModelFoundation where TEntity : class {

      protected TEntity entity;
      private IRepository<TEntity> repo;

      /// <summary>
      /// Contains all the properties of the model as well as the extended ones form the ViewModel
      /// </summary>
      public dynamic BindingData { get; set; }

      public ViewModelProxy(TEntity entity) : base() {
         this.entity = entity;
         this.repo = Injector.I.Get<IRepository<TEntity>>();
         BindingData = new DynamicProxy(entity);
         (BindingData as DynamicProxy).PropertyChanged += DynamicViewModel_PropertyChanged;
      }

      // Alert the ViewModelFoundation if a property has changed and persist the data
      private void DynamicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
         RaisePropertyChanged(e.PropertyName);
         repo.Persist(); // AsyncPersist isn't that good huh ?
      }
   }
}
