using GalaSoft.MvvmLight;
using Ninject.Modules;
using TestApp1.ViewModel;

namespace TestApp1.Ninject.Modules {
   class MainViewModelsModule : NinjectModule {

      public override void Load() {

         // Register all ViewModels
         Bind<CustomViewModelBase>().To<HelloViewModel>().InSingletonScope();
         Bind<CustomViewModelBase>().To<EmailsViewModel>().InSingletonScope();

      }
   }
}
