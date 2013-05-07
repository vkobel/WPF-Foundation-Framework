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
   public class EmailsViewModel : ViewModelBase {

      private static IRepository<Person> repo;
      private Person current;

      public ICollectionView ColView { get; set; }

      // ????????????????????????
      // Comment gérer les collections ???

      // public ObservableCollection<EmailsViewModel> All { get; set; } 

      public string EmailsStr {
         get {
            return string.Join(", ", (ColView.CurrentItem as Person).Emails.Select(e => e.Email1));
         }
      }

      public EmailsViewModel(IRepository<Person> rep) {
         repo = rep;
         //All = new ObservableCollection<EmailsViewModel>();
         ColView = CollectionViewSource.GetDefaultView(repo.GetAll());
      }

   }
}
