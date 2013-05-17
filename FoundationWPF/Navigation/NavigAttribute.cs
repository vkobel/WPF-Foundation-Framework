using System;

namespace FoundationWPF.Navigation {

   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
   public class NavigAttribute : Attribute {

      public NavigConfig MainConfig { get; private set; }
      public NavigConfig SubConfig { get; private set; }

      // Try to get configuration of navigation
      public NavigAttribute(string mainNavTitle, string subNavTitle = "") {
         MainConfig = NavigConfigLoader.GetConfig(mainNavTitle);
         if(!string.IsNullOrEmpty(subNavTitle))
            SubConfig = NavigConfigLoader.GetConfig(subNavTitle);
      }
   }
}
