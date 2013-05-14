using KobiDataFramework;
using KobiWPFFramework.Navigation;

namespace KobiWPFFramework.ViewModel {
   class PersonViewModel : ProxiedViewModel<Person> {
      
      public string Fullname { get { return BindingData.Firstname + " " + BindingData.Lastname; } }

      public PersonViewModel(Person p) : base(p) {
         var proxy = BindingData as DynamicProxy;
         proxy.Register(this);
         proxy.RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      }
   }

   [Navig("Ressources Humaines", "Persons")]
   class PersonsViewModel : ViewModelCollection<Person, PersonViewModel> {
   }

}
