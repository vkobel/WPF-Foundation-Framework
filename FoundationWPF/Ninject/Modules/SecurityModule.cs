using GalaSoft.MvvmLight;
using Ninject.Modules;
using FoundationWPF.ViewModel;
using FoundationWPF.Security;

namespace FoundationWPF.Ninject.Modules {
   class SecurityModule : NinjectModule {

      public override void Load() {

         Bind<CurrentUser>().ToSelf().InSingletonScope();
      }
   }
}
