using Ninject;
using Ninject.Modules;
using System;

namespace FoundationWPF.DI {

   /// <summary>
   /// Singleton wrapper to the dependency injection framework (here: Ninject)
   /// </summary>
   public class Injector {

      private static readonly Lazy<Injector> instance = new Lazy<Injector>(() => new Injector());
      protected IKernel Kernel { get; private set; }

      // private singleton ctor
      private Injector() {
         Kernel = new StandardKernel();
      }

      /// <summary>
      /// Wrapper method to load modules
      /// </summary>
      /// <param name="modules">an array of NinjectModules containing the binding configuration</param>
      public void LoadModules(params NinjectModule[] modules) {
         Kernel.Load(modules);
      }

      /// <summary>
      /// Current instance of this class
      /// </summary>
      public static Injector I {
         get { return instance.Value; }
      }

      /// <summary>
      /// Wrapper call to manually retrieve instances through the kernel
      /// </summary>
      /// <typeparam name="T">The type to retrieve</typeparam>
      /// <returns>The binded instace matching the type parameter</returns>
      public T Get<T>(){
         return Kernel.Get<T>();
      }

   }
}
