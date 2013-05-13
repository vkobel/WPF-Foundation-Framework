using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestApp1.ExtensionMethods {
   public static class GenericPropertyMethods {

      public static PropertyInfo GetProperty(this object obj, string name) {
         return obj.GetType().GetProperty(name);
      }

      public static bool HasProperty(this object obj, string name) {
         return obj.GetType().GetProperty(name) != null;
      }

      public static object GetPropertyValue(this object obj, string name) {
         return obj.GetType().GetProperty(name).GetValue(obj);
      }

      public static U GetPropertyValue<T, U>(this T obj, string name) where U : class {
         return typeof(T).GetProperty(name).GetValue(obj) as U;
      }

      public static T GetFirstWithProperty<T>(this List<T> objs, string name) {
         foreach(var o in objs)
            if(o.HasProperty(name)) return o;
         throw new Exception("There is no proxied property named '" + name + "' in the provided list");
      }

      public static object GetFirstMatchingPropertyValue(this List<object> objs, string name) {
         return objs.GetFirstWithProperty(name).GetPropertyValue(name);
      }

      public static void SetPropertyValue(this object obj, string name, object value) {
         obj.GetType().GetProperty(name).SetValue(obj, value);
      }

      public static void SetFirstMatchingPropertyValue(this List<object> objs, string name, object value) {
         objs.GetFirstWithProperty(name).SetPropertyValue(name, value);
      }

   }
}
