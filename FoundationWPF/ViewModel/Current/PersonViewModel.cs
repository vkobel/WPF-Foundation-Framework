using GalaSoft.MvvmLight.Command;
using FoundationData;
using FoundationWPF.Navigation;
using System.Windows.Input;
using FoundationWPF.Security;

namespace FoundationWPF.ViewModel {
   class PersonViewModel : ViewModelProxy<Person> {
      
      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         var proxy = BindingData as DynamicProxy; // simple cast to enable intellisense on the dynamic object (and compile-time verification)
         proxy.Register(this);
         proxy.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("Ressources Humaines", "Persons")]
   [Auth("TheMan")]
   class PersonCollectionViewModel : ViewModelCollection<Person, PersonViewModel> {
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
