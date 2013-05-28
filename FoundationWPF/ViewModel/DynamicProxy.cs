using FoundationData;
using FoundationWPF.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace FoundationWPF.ViewModel {

   using PropList = List<Tuple<string, string>>;

   /// <summary>
   /// Simple extended class to represent a changing property with additional params (oldVal, newVal, ...)
   /// </summary>
   public class FoundationPropertyChangingEventArgs : EventArgs {
      public virtual string PropertyName { get; private set; }
      public virtual object Entity { get; private set; }
      public virtual object OldValue { get; private set; }
      public virtual object NewValue { get; private set; }

      public FoundationPropertyChangingEventArgs(string propertyName, object entity, object oldValue, object newValue) {
         PropertyName = propertyName;
         Entity = entity;
         OldValue = oldValue;
         NewValue = newValue;
      }
   }

   /// <summary>
   /// Dynamic proxy class used to access multiple objects properties by a single proxy.
   /// i.e. extend a model object with custom properties of a viewmodel. It implements the interface INotifyPropertyChanged.
   /// </summary>
   public class DynamicProxy : DynamicObject, INotifyPropertyChanged, IDataErrorInfo {

      private List<object> proxiedObjs;
      // format: <real, dependency>
      private PropList propDependencies;

      /// <summary>
      /// Subscribe to this to know when a property has changed into the proxy.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      public event FoundationPropertyChangingEventHandler PropertyChanging;
      public delegate bool FoundationPropertyChangingEventHandler(object sender, FoundationPropertyChangingEventArgs e);

      /// <summary>
      /// Create a new instance of the proxy
      /// </summary>
      /// <param name="proxiedObjects">objects you want to be proxied</param>
      public DynamicProxy(params object[] proxiedObjects) {
         proxiedObjs = new List<object>(proxiedObjects);
         propDependencies = new PropList();
      }

      /// <summary>
      /// Register a new object into the proxy after the ctor has been called
      /// </summary>
      /// <param name="obj">The new object to be proxied</param>
      public void Register(object obj) {
         proxiedObjs.Add(obj);
      }

      /// <summary>
      /// Register a new dependency property. When PropertyChanged is raised, the object looks for a registered dependency
      /// and fires a PropertyChanged event for it. i.e. RegisterPropertyDependency("Fullname", "Firstname", "Lastname");
      /// tells that the "Fullname" property must be update when "Firstname" or "Lastname" is modified.
      /// </summary>
      /// <param name="propName">The name of the new property (non-existing in the model btw)</param>
      /// <param name="dependsOn">Any number of string the paramerter depends on</param>
      public void RegisterPropertyDependency(string propName, params string[] dependsOn) {
         foreach(var p in dependsOn)
            propDependencies.Add(new Tuple<string,string>(p, propName));
      }
      
      /// <summary>
      /// Called when a member (property) is called
      /// </summary>
      public override bool TryGetMember(GetMemberBinder binder, out object result) {
         result = proxiedObjs.GetFirstMatchingPropertyValue(binder.Name);
         return result != null;
      }

      /// <summary>
      /// Called when a member (property) is setted
      /// </summary>
      public override bool TrySetMember(SetMemberBinder binder, object value) {
         var modificationAllowed = PropertyChanging(this, new FoundationPropertyChangingEventArgs(binder.Name,
                                                                                      proxiedObjs.GetFirstWithProperty(binder.Name),
                                                                                      proxiedObjs.GetFirstMatchingPropertyValue(binder.Name),
                                                                                      value));
         if(modificationAllowed)
            proxiedObjs.SetFirstMatchingPropertyValue(binder.Name, value);
         
         //

         PropertyChanged(this, new PropertyChangedEventArgs(binder.Name));
         
         // Raise dependency properties notifications
         foreach(var prop in propDependencies.Where(p => p.Item1 == binder.Name))
            PropertyChanged(this, new PropertyChangedEventArgs(prop.Item2));
         return true;
      }

      /// <summary>
      /// Get the dependencies a property depends on
      /// </summary>
      /// <param name="prop">The property to be checked against the dependencies</param>
      /// <returns>A list of dependencies names</returns>
      public List<string> GetDependencies(string prop) {
         return (from dep in propDependencies
                 where dep.Item2 == prop
                 select dep.Item1).ToList();
      }

      /// <summary>
      /// Check if the parameter has a dependency or not
      /// </summary>
      /// <param name="prop">The property to be checked against the dependencies</param>
      /// <returns>True, if it's a dependency, otherwise false</returns>
      public bool HasDependency(string prop) {
         return propDependencies.Exists(p => p.Item2 == prop);
      }

      public string Error {
         get { throw new NotImplementedException(); }
      }

      /// <summary>
      /// Enable the support of IDataErrorInfo on the DynamicProxy
      /// Since it's a proxy it simply forward the indexer call to the model object
      /// </summary>
      string IDataErrorInfo.this[string columnName] {
         get {
            var entity = proxiedObjs.GetFirstWithProperty(columnName);
            if(entity != null && entity is IDataErrorInfo) // check if the entity implements the IDataErrorInfo interface
               return (string)(entity as IDataErrorInfo)[columnName];
            else
               return null;
         }
      }
   }
}
