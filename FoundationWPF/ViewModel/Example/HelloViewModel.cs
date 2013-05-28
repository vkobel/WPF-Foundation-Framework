using FoundationWPF.Navigation;
using System.Threading;

namespace FoundationWPF.ViewModel {

   [Navig("HR", "Hello")]
   [Navig("HR", "World")]
   //[Auth("TheMan")]
   public class HelloViewModel : ViewModelFoundation, IPreLoadable { 

      public string Name { get; set; }

      public HelloViewModel() {
         Name = "Hello World of Employés";
         IsPreLoadNeeded = true;
      }

      public ViewModelFoundation LoadingViewModel { get; set; }

      public bool IsPreLoadNeeded { get; set; }
      public bool IsCurrentlyLoading { get; set; }
      public void PreLoad() {
         Thread.Sleep(4000);
      }
      
   }
}
