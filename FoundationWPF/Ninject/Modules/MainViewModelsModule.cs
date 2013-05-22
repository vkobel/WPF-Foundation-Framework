using FoundationWPF.ViewModel;
using Ninject.Modules;

namespace FoundationWPF.Ninject.Modules {
   class MainViewModelsModule : NinjectModule {

      public override void Load() {

         // Bind the Loading ViewModel to any ViewModelFoundation variable that starts with "load"
         Bind<ViewModelFoundation>().To<LoadingViewModel>().When(req => req.Target.Name.StartsWith("load")).InSingletonScope();
         // Bind the Authentication ViewModel to any ViewModelFoundation variable that starts with "auth"
         Bind<ViewModelFoundation>().To<AuthenticationViewModel>().When(req => req.Target.Name.StartsWith("auth")).InSingletonScope();

         // Register all ViewModels
         Bind<ViewModelFoundation>().To<HelloViewModel>().InSingletonScope();
         Bind<ViewModelFoundation>().To<PersonCollectionViewModel>().InSingletonScope();
      }
   }
}
