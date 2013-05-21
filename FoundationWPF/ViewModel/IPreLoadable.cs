
namespace FoundationWPF.ViewModel {
   
   interface IPreLoadable {
      bool IsPreLoadNeeded { get; set; }
      bool IsCurrentlyLoading { get; set; }
      ViewModelFoundation LoadingViewModel { get; set; }
      void PreLoad();
   }
}
