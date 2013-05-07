using Data;
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
   public class EmailsViewModel : CustomViewModelBase {

      private static IRepository<Person> repo;

      public ICollectionView ColView { get; set; }
      public ObservableCollection<Person> All { get; set; }

      public EmailsViewModel Current {
         get {
            return ColView.CurrentItem as EmailsViewModel;
         }
      }

      // public string Fullname { get { return Current.person.Firstname + " " + Current.person.Lastname; } }

      /*
      public string EmailsStr {
         get {
            return string.Join(", ", Current.Emails.Select(e => e.Email1));
         }
      }
      */

      public EmailsViewModel(IRepository<Person> rep) {
         repo = rep;

         All = new ObservableCollection<Person>(repo.GetAll());



         ColView = CollectionViewSource.GetDefaultView(All);
         ColView.CurrentChanged += ColView_CurrentChanged;
      }

      public override void PersistData() {
         repo.Persist();
      }

      void ColView_CurrentChanged(object sender, System.EventArgs e) {
         PersistData();
      }
   }
}
