using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.GenericRepo;
using System.ComponentModel;
using TestApp1.ExtensionMethods;

namespace TestApp1.ViewModel {
   class DynamicBinderProxy<T> : DynamicObject, INotifyPropertyChanged where T: class {

      private T Rep { get; set; }
      private IRepository<T> re;

      public event PropertyChangedEventHandler PropertyChanged;

      public DynamicBinderProxy(T r, IRepository<T> repo) {
         Rep = r;
         re = repo;
      }

      public override bool TryGetMember(GetMemberBinder binder, out object result) {
         result = Rep.GetPropValue(binder.Name);
         //result = result ?? 
         return result != null;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value) {
         Rep.GetType().GetProperty(binder.Name).SetValue(Rep, value);
         PropertyChanged(this, new PropertyChangedEventArgs(binder.Name));
         re.Persist();
         return true;
      }
   }
}
