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

   public class EmailsViewModel : ViewModelBase {

      private static IRepository<Person> repo;

      public ICollectionView ColView { get; set; }
      public ObservableCollection<Person> All { get; set; }

      public EmailsViewModel Current {
         get {
            return ColView.CurrentItem as EmailsViewModel;
         }
      }

      public EmailsViewModel(IRepository<Person> rep) {
         repo = rep;

         // Revoir méthode de loading (lazy)

         All = new ObservableCollection<Person>(repo.GetAll());

         ColView = CollectionViewSource.GetDefaultView(All);
      }

   }
}
