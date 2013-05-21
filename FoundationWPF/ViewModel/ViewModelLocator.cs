/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TestApp1"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/
using FoundationWPF.Ninject;
using FoundationWPF.Security;

namespace FoundationWPF.ViewModel {

   /// <summary>
   /// This class contains static references to all the view models in the
   /// application and provides an entry point for the bindings.
   /// </summary>
   public class ViewModelLocator {

      public ApplicationViewModel Main {
         get {
            // Login current user
            Nj.I.Get<CurrentUser>().Login();
            return Nj.I.Get<ApplicationViewModel>(); 
         }
      }

      public static void Cleanup() {
         // TODO Clear the ViewModels
      }
   }
}