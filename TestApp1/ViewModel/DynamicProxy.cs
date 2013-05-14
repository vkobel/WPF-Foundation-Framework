using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using KobiWPFFramework.ExtensionMethods;

namespace KobiWPFFramework.ViewModel {

   using PropDep = List<Tuple<string, string>>;

   class DynamicProxy<T> : DynamicObject, INotifyPropertyChanged where T: class {

      private List<object> proxiedObjs;
      private PropDep propDependencies;

      public event PropertyChangedEventHandler PropertyChanged;

      public DynamicProxy(params object[] proxiedObjects) {
         proxiedObjs = new List<object>(proxiedObjects);
         propDependencies = new PropDep();
      }

      public void RegisterProxiedObj(object obj) {
         proxiedObjs.Add(obj);
      }

      public void RegisterPropertyDependency(string propName, params string[] dependsOn) {
         foreach(var p in dependsOn)
            propDependencies.Add(new Tuple<string,string>(p, propName));
      }

      public override bool TryGetMember(GetMemberBinder binder, out object result) {
         result = proxiedObjs.GetFirstMatchingPropertyValue(binder.Name);
         return result != null;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value) {
         proxiedObjs.SetFirstMatchingPropertyValue(binder.Name, value);
         PropertyChanged(this, new PropertyChangedEventArgs(binder.Name));
         // Raise dependency properties notifications
         foreach(var prop in propDependencies.Where(p => p.Item1 == binder.Name))
            PropertyChanged(this, new PropertyChangedEventArgs(prop.Item2));
         return true;
      }


   }
}
