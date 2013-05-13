using Data.GenericRepo;
using GalaSoft.MvvmLight;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestApp1.Ninject;

namespace TestApp1.ViewModel {
   class StorageViewModel<TEntity, TViewModel> : ViewModelBase where TEntity : class 
                                                               where TViewModel : DynamicViewModel<TEntity> {
   
      protected ObservableCollection<TViewModel> all { get; set; }
      public ICollectionView ColView { get; set; }

      private IRepository<TEntity> repo { get; set; }

      public StorageViewModel() {
         all = new ObservableCollection<TViewModel>();
         ColView = CollectionViewSource.GetDefaultView(all);
         repo = Nj.I.Get<IRepository<TEntity>>();
         foreach(var ent in repo.GetAll())
            all.Add(Activator.CreateInstance(typeof(TViewModel), ent) as TViewModel);
      }
   
   }
}
