using System.Threading;

namespace FoundationWPF.Security {

   /// <summary>
   /// Represent the current user of the application, usually a good practice to make it a singleton
   /// (i.e. in a Ninject module)
   /// </summary>
   public class CurrentUser : SecurityObject {

      public string Name { get; set; }

      public CurrentUser() : base() { 
      }

      public override void Login() {
         // Login stuff

         //Thread.Sleep(6000);
         Roles.Add("TheMan");
         Name = "Vincent Kobel";
      }
   }
}
