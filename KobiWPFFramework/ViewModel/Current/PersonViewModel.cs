﻿using GalaSoft.MvvmLight.Command;
using KobiDataFramework;
using KobiWPFFramework.Navigation;
using System.Windows.Input;

namespace KobiWPFFramework.ViewModel {
   class PersonViewModel : ProxiedViewModel<Person> {
      
      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         var proxy = BindingData as DynamicProxy; // simple cast to get intellisense on the dynamic (and compile verification)
         proxy.Register(this);
         proxy.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("Ressources Humaines", "Persons")]
   class PersonsViewModel : ViewModelCollection<Person, PersonViewModel> {
      /*
      public PersonsViewModel() : base(p => p.Id <= 2){
      }
      */

      private ICommand sortCmd;
      public ICommand SortCmd {
         get {
            if(sortCmd == null)
               sortCmd = new RelayCommand(() => 
                  CollectionView.SortDescriptions.Add(new System.ComponentModel.SortDescription("BindingData.Lastname", System.ComponentModel.ListSortDirection.Ascending))
               );
            return sortCmd;
         }
      }

   }

}
