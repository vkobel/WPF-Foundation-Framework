using System;

namespace TestApp1.Navigation {

   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
   public class NavigAttribute : Attribute {

      public NavigConfig MainConfig { get; private set; }
      public NavigConfig SubConfig { get; private set; }

      public NavigAttribute(string mainNavTitle, string subNavTitle = "") {
         MainConfig = NavigConfigLoader.GetConfig(mainNavTitle);
         if(!string.IsNullOrEmpty(subNavTitle))
            SubConfig = NavigConfigLoader.GetConfig(subNavTitle);
      }
   }
}
