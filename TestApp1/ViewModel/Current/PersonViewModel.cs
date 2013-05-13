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

      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         BindingData.RegisterProxiedObj(this);
         BindingData.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("Ressources Humaines", "Persons")]
   class PersonsViewModel : StorageViewModel<Person, PersonViewModel> {
   }

}
