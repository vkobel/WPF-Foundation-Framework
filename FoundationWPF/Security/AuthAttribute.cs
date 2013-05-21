using System;
using System.Collections.Generic;

namespace FoundationWPF.Security {

   /// <summary>
   /// Specifiy that a ViewModel uses authentication.
   /// </summary>
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
   class AuthAttribute : Attribute {

      public List<string> AllowedRoles { get; private set; }

      public AuthAttribute(params string[] roles) {
         AllowedRoles = new List<string>(roles);
      }
   }
}
