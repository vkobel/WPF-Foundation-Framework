using FoundationWPF.ViewModel;
using Ninject.Modules;

namespace FoundationWPF.Ninject.Modules {
   class MainViewModelsModule : NinjectModule {

      public override void Load() {

         // Loading screen ViewModel
         Bind<LoadingViewModel>().ToSelf().InSingletonScope();

         // Register all ViewModels
         Bind<ViewModelFoundation>().To<HelloViewModel>().InSingletonScope();
         Bind<ViewModelFoundation>().To<PersonCollectionViewModel>().InSingletonScope();
      }
   }
}
