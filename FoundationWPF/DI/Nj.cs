using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FoundationWPF.DI.Modules;

namespace FoundationWPF.DI {

   /// <summary>
   /// Singleton object to access the Ninject's kernel
   /// </summary>
   public class Nj {

      private static readonly Lazy<Nj> instance = new Lazy<Nj>(() => new Nj());
      public IKernel Kernel { get; private set; }

      private Nj() {
         Kernel = new StandardKernel(new SecurityModule(),
                                     new RepositoriesModule(),
                                     new ViewModelsModule(),
                                     new NavigationModule());
      }

      public static Nj I {
         get { return instance.Value; }
      }

      public T Get<T>(){
         return Kernel.Get<T>();
      }
   
   }
}
