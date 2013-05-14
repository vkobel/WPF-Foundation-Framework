using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KobiWPFFramework.Navigation;

namespace KobiWPFFramework.ViewModel {

   [Navig("Ressources Humaines", "Employés")]
   [Navig("Ressources Humaines", "Employés détails")]
   public class HelloViewModel : ViewModelBase {

      public string Name { get; set; }

      public HelloViewModel() {
         Name = "Hello World of Employés";
      }

   }
}
