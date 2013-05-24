using FoundationWPF.DI;
using FoundationWPF.Security;
using GalaSoft.MvvmLight;
using System;
using System.Linq;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// Simple wrapper class for ViewModelBase of MVVM Light Toolkit
   /// </summary>
   public abstract class ViewModelFoundation : ViewModelBase {

      public SecurityObject CurrentUser { get; private set; }
      public event EventHandler ViewModelDisplayed;

      public ViewModelFoundation(){

         CurrentUser = Injector.I.Get<SecurityObject>();

         /// Default behavior: if the ViewModel is displayed without proper access rights 
         /// it throws an exception (for the final user, the program will crash).
         ViewModelDisplayed += (sender, e) => {
            if(!(sender as ViewModelFoundation).IsAuthorized(CurrentUser))
               throw new Exception("Insufficient authorization level");
         };
      }

      /// <summary>
      /// Check if the specified vmType has an AuthAttribute and if the SecurityObject has the proper role(s)
      /// </summary>
      /// <param name="securityObject">The security object containing the Roles (typically the current user)</param>
      /// <returns>true if the securityObject is able to access the vmType, then false</returns>
      public bool IsAuthorized(SecurityObject securityObject) {
         AuthAttribute aa = this.GetType().GetCustomAttributes(typeof(AuthAttribute), inherit: true).FirstOrDefault() as AuthAttribute;
         if(aa != null && securityObject != null && securityObject.Roles != null)
            return securityObject.Roles.Any(r => aa.AllowedRoles.Contains(r));
         return true;
      }

      /// <summary>
      /// Raise a ViewModelDisplayed event to alert the subscribers that a new ViewModel 
      /// has been loaded into the application
      /// </summary>
      /// <param name="sender">The ViewModel that will be displayed</param>
      protected virtual void RaiseViewModelDisplayed(ViewModelFoundation sender) {
         EventHandler handler = sender.ViewModelDisplayed;
         if(handler != null)
            handler(sender, EventArgs.Empty);
      }

   }
}
