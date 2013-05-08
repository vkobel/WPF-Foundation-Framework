using Data.GenericRepo;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using TestApp1.ExtensionMethods;

namespace TestApp1.ViewModel {
   public abstract class DynamicViewModel<T> : ViewModelBase where T : class {

      protected T entity;
      private IRepository<T> repo;

      public dynamic BindingData { get; set; }

      public DynamicViewModel(T entity, IRepository<T> repo) {
         this.entity = entity;
         this.repo = repo;
         BindingData = new DynamicBinderProxy<T>(entity, repo);
         (BindingData as INotifyPropertyChanged).PropertyChanged += DynamicViewModel_PropertyChanged;
      }

      void DynamicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
         RaisePropertyChanged(e.PropertyName);
      }     
   }
}
