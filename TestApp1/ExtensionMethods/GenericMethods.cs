using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1.ExtensionMethods {
   public static class GenericMethods {

      public static object GetPropValue<T>(this T obj, string name) {
         return typeof(T).GetProperty(name).GetValue(obj);
      }

      public static U GetPropValue<T, U>(this T obj, string name) where U : class {
         return typeof(T).GetProperty(name).GetValue(obj) as U;
      }
   
   }
}
