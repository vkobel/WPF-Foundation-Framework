using GalaSoft.MvvmLight;
using Ninject.Modules;
using KobiWPFFramework.ViewModel;

namespace KobiWPFFramework.Ninject.Modules {
   class MainViewModelsModule : NinjectModule {

      public override void Load() {

         // Loading screen ViewModel
         Bind<LoadingViewModel>().ToSelf().InSingletonScope();

         // Register all ViewModels
         Bind<ViewModelBase>().To<HelloViewModel>().InSingletonScope();
         Bind<ViewModelBase>().To<PersonsViewModel>().InSingletonScope();
      }
   }
}
