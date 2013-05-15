using GalaSoft.MvvmLight;
using KobiWPFFramework.Navigation;
using KobiWPFFramework.Ninject;
using System.Threading;

namespace KobiWPFFramework.ViewModel {

   [Navig("Ressources Humaines", "Employés")]
   [Navig("Ressources Humaines", "Employés détails")]
   public class HelloViewModel : ViewModelBase { //, IPreLoadable { 

      public string Name { get; set; }

      public HelloViewModel() {
         Name = "Hello World of Employés";
         IsPreLoadNeeded = true;
         LoadingViewModel = Nj.I.Get<LoadingViewModel>();
      }

      public bool IsPreLoadNeeded { get; set; }
      public bool IsCurrentlyLoading { get; set; }
      public void PreLoad() {
         Thread.Sleep(4000);
      }
      public ViewModelBase LoadingViewModel { get; set; }
   }
}
