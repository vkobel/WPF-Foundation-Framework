using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp1.Navigation;

namespace TestApp1.ViewModel {

   [Navig("Ressources Humaines", "Employés")]
   [Navig("Ressources Humaines", "Employés détails")]
   public class HelloViewModel : ViewModelBase {

      public string Name { get; set; }

      public HelloViewModel() {
         Name = "Hello";
      }

   }
}
