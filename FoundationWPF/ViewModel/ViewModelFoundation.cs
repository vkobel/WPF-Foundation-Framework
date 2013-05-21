using FoundationWPF.Ninject;
using FoundationWPF.Security;
using GalaSoft.MvvmLight;
using System;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// Simple wrapper class for ViewModelBase of MVVM Light Toolkit
   /// </summary>
   public abstract class ViewModelFoundation : ViewModelBase {

      public ViewModelFoundation() {
         foreach(AuthAttribute aa in this.GetType().GetCustomAttributes(typeof(AuthAttribute), inherit: true)) {
            var plop = Nj.I.Get<CurrentUser>();
            var i = 0;
         }
      }
   
   }
}
