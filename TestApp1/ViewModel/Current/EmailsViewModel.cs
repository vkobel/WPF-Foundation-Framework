﻿using Data;
using Data.GenericRepo;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;
using TestApp1.Navigation;

namespace TestApp1.ViewModel {

   [Navig("Timesheet", "Agenda")]
   [Navig("Ressources Humaines", "Super")]
   public class EmailsViewModel : ViewModelBase {

      private static IRepository<Person> repo;

      public ICollectionView ColView { get; set; }
      public ObservableCollection<Person> All { get; set; }

      public Person Current {
         get {
            return ColView.CurrentItem as Person;
         }
      }

      public string Fullname { get { return Current.Firstname + " " + Current.Lastname; } }

      public string EmailsStr {
         get {
            return string.Join(", ", Current.Emails.Select(e => e.Email1));
         }
      }

      public EmailsViewModel(IRepository<Person> rep) {
         repo = rep;
         All = new ObservableCollection<Person>(repo.GetAll());
         ColView = CollectionViewSource.GetDefaultView(All);
         var i = 0;
      }

   }
}
