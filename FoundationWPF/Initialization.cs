using FoundationWPF.Ninject;
using FoundationWPF.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundationWPF {

   /// <summary>
   /// The Init method is called at the creation of the application, do every init stuff you need into it
   /// </summary>
   public static class Initialization {

      public static void Init() { 
         
         // Put your init logic in here, like login the user
         
         // Login the current user
         Nj.I.Get<CurrentUser>().Login();

      }

   }
}
