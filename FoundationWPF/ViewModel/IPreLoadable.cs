
using GalaSoft.MvvmLight;
namespace FoundationWPF.ViewModel {
   
   interface IPreLoadable {
      bool IsPreLoadNeeded { get; set; }
      bool IsCurrentlyLoading { get; set; }
      ViewModelBase LoadingViewModel { get; set; }
      void PreLoad();
   }
}
