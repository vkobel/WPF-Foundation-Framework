using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KobiWPFFramework.Navigation;
using KobiWPFFramework.Navigation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KobiWPFFramework.ViewModel {

   /// <summary>
   /// Represents the current state of the application and manages a two levels navigation system.
   /// Sort of an Orchestrator.
   /// </summary>
   public class ApplicationViewModel : ViewModelBase {

      #region private fields
      private ICommand changeViewCmd;
      private ICommand changeMainCmd;

      private ViewModelBase currentViewModel;
      private NavigConfig currentMainNav;

      private List<NavigConfig> navStructure;
      #endregion

      #region Properties
      // Currently selected ViewModel
      public ViewModelBase CurrentViewModel {
         get { return currentViewModel; }
         set {
            if(currentViewModel != value) {
               currentViewModel = value;
               // If the current VM need
               if(currentViewModel is IPreLoadable) {
                  var cvm = currentViewModel as IPreLoadable;
                  if(cvm.IsPreLoadNeeded())
                     AsyncLoadViewModel(cvm);
               } else {
                  RaisePropertyChanged("CurrentViewModel");
               }
            }
         }
      }

      private async void AsyncLoadViewModel(IPreLoadable vm) {
         
         var swap = currentViewModel;
         currentViewModel = new LoadingViewModel();
         RaisePropertyChanged("CurrentViewModel");
         
         await Task.Factory.StartNew(() => vm.PreLoad());
         currentViewModel = swap;
         RaisePropertyChanged("CurrentViewModel");
      }

      // Currently selected main navig
      public NavigConfig CurrentMainNav {
         get { return currentMainNav; } 
         set {
            if(currentMainNav != value) {
               currentMainNav = value;
               CurrentViewModel = SubNavig[0].VM;
               RaisePropertyChanged("CurrentMainNav");
               RaisePropertyChanged("SubNavig");
            }
         }
      }

      // List of main navig properties
      public List<NavigConfig> MainNavig {
         get {
            return (from elem in navStructure
                    where elem.Enabled
                    orderby elem.Position
                    select elem).Distinct().ToList();
         }
      }

      /// List of current sub-viewmodels or if there is no subVMs return the VM of the MainNavig
      public List<NavigConfig> SubNavig {
         get {
            if(CurrentMainNav.SubConfig.Count > 0)
               return (from elem in CurrentMainNav.SubConfig
                       where elem.Enabled
                       orderby elem.Position
                       select elem).ToList();
            else
               return new List<NavigConfig> { CurrentMainNav };
         }
      }
      #endregion

      // ctor: injection of all registred viewmodels (in MainViewModelsModule.cs)
      /// <summary>
      /// Instanciate the ApplicationViewModel. All registred ViewModels are injected into the param mainViewModels
      /// </summary>
      /// <param name="mainViewModels">An array of the application's ViewModels (injected)</param>
      public ApplicationViewModel(ViewModelBase[] mainViewModels) {

         // Load navigation informations
         NavigConfigLoader.RegisterConfigurations(new RH(), new Emp(), new EmpDetails(),
                                                  new TS(), new Agenda());
         navStructure = new List<NavigConfig>();

         // Browse every ViewModels and search if it has a Navig attribute
         foreach(var vm in mainViewModels) {
            foreach(Attribute attr in vm.GetType().GetCustomAttributes(inherit: false)) {
               if(attr is NavigAttribute) {
                  var na = attr as NavigAttribute;
                  NavigConfig mainConf;
                  
                  // Check if it exists a NavigConf with the same name
                  var existingMainConf = navStructure.SingleOrDefault(n => n.Name == na.MainConfig.Name);

                  if(existingMainConf != null) // If it's the case we reference it
                     mainConf = existingMainConf;
                  else {                       // Else we use the MainConfig as a new elem of navStructure
                     mainConf = na.MainConfig;
                     navStructure.Add(mainConf);
                  }

                  // Assign the VM to the SubConfig or the main
                  if(na.SubConfig != null) {
                     na.SubConfig.VM = vm;
                     mainConf.SubConfig.Add(na.SubConfig);
                  } else {
                     mainConf.VM = vm;
                  }
               }
            }
         }
         CurrentMainNav = MainNavig[0];
         CurrentViewModel = SubNavig[0].VM;
      }

      #region Commands
      public ICommand ChangeMainCmd {
         get {
            if(changeMainCmd == null)
               changeMainCmd = new RelayCommand<NavigConfig>(nc => CurrentMainNav = nc);
            return changeMainCmd;
         }
      }

      public ICommand ChangeViewCmd {
         get {
            if(changeViewCmd == null)
               changeViewCmd = new RelayCommand<ViewModelBase>(vm => CurrentViewModel = vm);
            return changeViewCmd;
         }
      }
      #endregion
   }
}
