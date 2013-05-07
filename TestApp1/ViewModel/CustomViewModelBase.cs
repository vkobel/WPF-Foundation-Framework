using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.GenericRepo;

namespace TestApp1.ViewModel {
   public abstract class CustomViewModelBase : ViewModelBase {

      public abstract void PersistData();

   }
}
