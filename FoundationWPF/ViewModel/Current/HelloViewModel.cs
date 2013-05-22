using FoundationWPF.Navigation;
using FoundationWPF.Ninject;
using System.Threading;

namespace FoundationWPF.ViewModel {

   [Navig("Ressources Humaines", "Employés")]
   [Navig("Ressources Humaines", "Employés détails")]
   //[Auth("TheMan")]
   public class HelloViewModel : ViewModelFoundation { //, IPreLoadable { 

      public string Name { get; set; }

      public HelloViewModel(ViewModelFoundation loadingVm) {
         Name = "Hello World of Employés";
         IsPreLoadNeeded = true;
         LoadingViewModel = loadingVm;
      }

      public ViewModelFoundation LoadingViewModel { get; set; }

      public bool IsPreLoadNeeded { get; set; }
      public bool IsCurrentlyLoading { get; set; }
      public void PreLoad() {
         Thread.Sleep(4000);
      }
      
   }
}
