
using GalaSoft.MvvmLight;
namespace KobiWPFFramework.ViewModel {
   
   interface IPreLoadable {
      bool IsPreLoadNeeded { get; set; }
      bool IsCurrentlyLoading { get; set; }
      ViewModelBase LoadingViewModel { get; set; }
      void PreLoad();
   }
}
