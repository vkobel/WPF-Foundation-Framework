using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TestApp1.Ninject.Modules;

namespace TestApp1.Ninject {

   /// <summary>
   /// Singleton object to access the Ninject's kernel
   /// </summary>
   public class Nj {
      
      private static Nj instance;
      public IKernel Kernel { get; private set; }

      private Nj() {
         Kernel = new StandardKernel(new RepositoriesModule(),
                                     new MainViewModelsModule());
      }

      public static Nj I {
         get {
            if (instance == null)
               instance = new Nj();
            return instance;
         }
      }

      public T Get<T>(){
         return Kernel.Get<T>();
      }
   
   }
}
