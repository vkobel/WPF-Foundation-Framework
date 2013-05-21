using FoundationWPF.Security;
using Ninject.Modules;

namespace FoundationWPF.Ninject.Modules {
   class SecurityModule : NinjectModule {

      public override void Load() {
         //Bind<SecurityObject>().To<CurrentUser>().When(req => req.Target.Member.Name.ToLower() == "currentuser").InSingletonScope();
         Bind<CurrentUser>().ToSelf().InSingletonScope();
      }
   }
}
