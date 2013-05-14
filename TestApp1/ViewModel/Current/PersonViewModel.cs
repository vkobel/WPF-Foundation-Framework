using KobiDataFramework;
using KobiDataFramework.GenericRepo;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using KobiWPFFramework.Navigation;
using KobiWPFFramework.Ninject;

namespace KobiWPFFramework.ViewModel {
   class PersonViewModel : DynamicViewModel<Person> {
      
      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         BindingData.RegisterProxiedObj(this);
         BindingData.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("Ressources Humaines", "Persons")]
   class PersonsViewModel : ViewModelCollection<Person, PersonViewModel> {
   }

}
