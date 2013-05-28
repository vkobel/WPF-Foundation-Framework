using FoundationData.GenericRepo;
using FoundationWPF.DI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

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

      /// <summary>
      /// Simple typed alias for the dynamic object BindingData
      /// </summary>
      private DynamicProxy bindingData;

      public ViewModelProxy(TEntity entity) : base() {
         this.entity = entity;
         this.repo = Injector.I.Get<IRepository<TEntity>>();
         BindingData = new DynamicProxy(entity);
         bindingData = BindingData;
         bindingData.PropertyChanged += DynamicViewModel_PropertyChanged;
         bindingData.PropertyChanging += ViewModelProxy_PropertyChanging;
      }
      
      /// <summary>
      /// Check if the changing property has already been changed by someone else, and prompt for the required action
      /// </summary>
      /// <returns>true, if the value will be replaced by the user's one. False if the database value wins</returns>
      private bool ViewModelProxy_PropertyChanging(object sender, FoundationPropertyChangingEventArgs e) {

         if(e.OldValue != e.NewValue){ // if the value has been update
            var updatedValue = repo.GetReloadedProperty(e.Entity as TEntity, e.PropertyName);

            if(!e.OldValue.Equals(updatedValue) && !e.NewValue.Equals(updatedValue)) {
               string msg = string.Format("This field has a more recent value: '{0}'\nDo you want to replace this value ({0}) by yours ({1}) ?", updatedValue, e.NewValue);
               return DialogResult.Yes == MessageBox.Show(msg, "Recent update notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
         }
         return true;
      }

      // Alert the ViewModelFoundation if a property has changed and persist the data
      private void DynamicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {

         if(!bindingData.HasDependency(e.PropertyName)) {                             // Is not a dependency property
            if(string.IsNullOrEmpty((bindingData as IDataErrorInfo)[e.PropertyName])) // Explicit error checking: does not have any error
               repo.Persist();

         }
      }
   }
}
