using FoundationData.GenericRepo;
using FoundationWPF.DI;
using System.ComponentModel;
using System.Windows.Forms;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// Represents a generic ViewModel for a specific model entity. Often, this is the object exposed to the view
   /// directly, or via the ViewModelCollection.
   /// </summary>
   /// <typeparam name="TEntity">The type of the model entity (to create the repository)</typeparam>
   public abstract class ViewModelProxy<TEntity> : ViewModelFoundation 
                                                   where TEntity : class {

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
         (BindingData as DynamicProxy).PropertyChanging += ViewModelProxy_PropertyChanging;
      }
      
      /// <summary>
      /// Check if the changing property has already been changed by someone else, and prompt for the required action
      /// </summary>
      /// <returns>true, if the value will be replaced by the user's one. False if the database value wins</returns>
      private bool ViewModelProxy_PropertyChanging(object sender, FoundationPropertyChangingEventArgs e) {

         if(e.OldValue != e.NewValue){ // if the value has been update
            var updatedValue = repo.GetReloadedProperty(e.Entity as TEntity, e.PropertyName);

            if(!e.OldValue.Equals(updatedValue) && !e.NewValue.Equals(updatedValue)) {
               string msg = string.Format("This field has a more recent value: '{0}'\nDo you want to replace your value '{1}' by '{0}' ?", updatedValue, e.NewValue);
               return DialogResult.No == MessageBox.Show(msg, "Recent update notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
         }
         return true;
      }

      // Alert the ViewModelFoundation if a property has changed and persist the data
      private void DynamicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
         RaisePropertyChanged(e.PropertyName);
         repo.Persist(); // AsyncPersist isn't that good huh ?
      }
   }
}
