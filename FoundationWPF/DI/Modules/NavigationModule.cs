using FoundationWPF.Navigation;
using Ninject.Modules;

namespace FoundationWPF.DI.Modules {
   class NavigationModule : NinjectModule {

      public override void Load() {

         /// ---------------------------------------
         /// TODO : Register all navig configuration
         /// ---------------------------------------
         Bind<NavigConfig>().To<RH>();
         Bind<NavigConfig>().To<Persons>();
         Bind<NavigConfig>().To<Hello>();
         Bind<NavigConfig>().To<World>();

      }
   }
}
