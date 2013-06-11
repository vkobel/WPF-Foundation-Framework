using FoundationData;
using FoundationWPF.Navigation;
using FoundationWPF.Security;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;

namespace FoundationWPF.ViewModel {
   class PersonViewModel : ViewModelProxy<Person> {
      
      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         bindingData.Register(this);
         bindingData.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("HR", "Persons")]
   [Navig("Clients", "Internal")]
   [Auth("TheMan")]
   class PersonCollectionViewModel : ViewModelCollection<Person, PersonViewModel> {

      public PersonCollectionViewModel() : base() {
         // Filter(p => p.Id <= 2);
      }

      private ICommand sortCmd;
      public ICommand SortCmd {
         get {
            if(sortCmd == null)
               sortCmd = new RelayCommand(() => 
                  CollectionView.SortDescriptions.Add(new SortDescription("BindingData.Lastname", ListSortDirection.Ascending))
               );
            return sortCmd;
         }
      } 

   }

}
