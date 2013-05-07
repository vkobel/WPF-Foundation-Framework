using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestApp1.Navigation;
using TestApp1.Navigation.Config;

namespace TestApp1.ViewModel {

   public class ApplicationViewModel : ViewModelBase {

      private ICommand changeViewCmd;
      private ICommand changeMainCmd;

      private CustomViewModelBase currentViewModel;
      private NavigConfig currentMainNav;

      private List<NavigConfig> navStructure;

      // Currently selected ViewModel
      public CustomViewModelBase CurrentViewModel {
         get { return currentViewModel; }
         set {
            if(currentViewModel != value) {
               currentViewModel = value;
               currentViewModel.PersistData();
               RaisePropertyChanged("CurrentViewModel");
            }
         }
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

      // ctor: injection of all registred viewmodels (in MainViewModelsModule.cs)
      public ApplicationViewModel(CustomViewModelBase[] mainViewModels) {

         // Load navigation informations
         NavigConfigLoader.RegisterConfigurations(new RH(), new Emp(), new EmpDetails(),
                                                  new TS(), new Agenda());

         navStructure = new List<NavigConfig>();

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
               changeViewCmd = new RelayCommand<CustomViewModelBase>(vm => CurrentViewModel = vm);
            return changeViewCmd;
         }
      }

   }
}
