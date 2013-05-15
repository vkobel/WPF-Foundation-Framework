using FoundationData;
using FoundationData.GenericRepo;
using Ninject.Modules;
using System.Data.Entity;

namespace FoundationWPF.Ninject.Modules {
   class RepositoriesModule : NinjectModule {

      public override void Load() {
         
         /// Allows the binding of IRepository by injecting the defined DbContext
         /// into the Repository<T> constructor.
         Bind<DbContext>().To<vincentEntities>().InSingletonScope();
         
         /// Bind all the generic IRepository<T> to the corresponding Repository<T>.
         /// The constructor parameter of Repository<T> (ctx) is automatically injected
         Bind(typeof(IRepository<>)).To(typeof(Repository<>));

      }
   }
}
