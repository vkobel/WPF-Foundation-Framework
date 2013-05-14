﻿using KobiDataFramework.GenericRepo;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using KobiWPFFramework.Ninject;

namespace KobiWPFFramework.ViewModel {
   public abstract class DynamicViewModel<T> : ViewModelBase where T : class {

      protected T entity;
      private IRepository<T> repo;

      public dynamic BindingData { get; set; }

      public DynamicViewModel(T entity) {
         this.entity = entity;
         this.repo = Nj.I.Get<IRepository<T>>();
         BindingData = new DynamicProxy<T>(entity);
         (BindingData as INotifyPropertyChanged).PropertyChanged += DynamicViewModel_PropertyChanged;
      }

      private void DynamicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
         RaisePropertyChanged(e.PropertyName);
         if(sender != null)
            repo.Persist(); // AsyncPersist isn't that good huh ?
      }

   }
}