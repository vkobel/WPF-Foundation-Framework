using System.Collections.Generic;

namespace FoundationWPF.Security {

   /// <summary>
   /// Represent a security object by the Roles it holds. Usually, this type is binded (IoC) 
   /// to only one singleton object representing the current user of the application.
   /// </summary>
   public abstract class SecurityObject {

      /// <summary>
      /// List of string that contains the roles associated with this SecurityObject
      /// </summary>
      public List<string> Roles { get; set; }

      /// <summary>
      /// Login initialization stuff, should populate the Roles property
      /// </summary>
      public abstract void Login();
   
      public SecurityObject(){
         Roles = new List<string>();
      }

   }
}
