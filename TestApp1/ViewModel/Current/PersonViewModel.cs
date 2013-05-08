using Data;
using Data.GenericRepo;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TestApp1.Navigation;
using TestApp1.Ninject;

namespace TestApp1.ViewModel {
   class PersonViewModel : DynamicViewModel<Person> {
      public PersonViewModel(Person p) : base(p, Nj.I.Get<IRepository<Person>>()) {
         
         // Custom properties goes here !!!
         
         //BindingData.Fullname = { }
      }
   }

   [Navig("Cool", "Super")]
   [Navig("Ressources Humaines", "Toto")]
   class PersonRepoViewModel : ViewModelBase {

      public ICollectionView ColView { get; set; }
      public ObservableCollection<PersonViewModel> All { get; set; }

      public PersonRepoViewModel(IRepository<Person> repo) {
         All = new ObservableCollection<PersonViewModel>();
         foreach(var ent in repo.GetAll())
            All.Add(new PersonViewModel(ent));
         ColView = CollectionViewSource.GetDefaultView(All);
      }

   }

}
