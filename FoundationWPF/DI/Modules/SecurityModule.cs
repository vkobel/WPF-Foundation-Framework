using FoundationWPF.Security;
using Ninject.Modules;

namespace FoundationWPF.DI.Modules {
   class SecurityModule : NinjectModule {

      public override void Load() {
         Bind<CurrentUser>().ToSelf().InSingletonScope();
      }
   }
}
