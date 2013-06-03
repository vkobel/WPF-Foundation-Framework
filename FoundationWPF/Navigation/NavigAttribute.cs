using System;
using System.Collections.Generic;

namespace FoundationWPF.Navigation {

   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
   public class NavigAttribute : Attribute {

      private static Dictionary<string, NavigConfig> registredConfigs = new Dictionary<string, NavigConfig>();

      public NavigConfig MainConfig { get; private set; }
      public NavigConfig SubConfig { get; private set; }

      // Try to get configuration of navigation
      public NavigAttribute(string mainNavTitle, string subNavTitle = "") {
         MainConfig = GetConfig(mainNavTitle);
         if(!string.IsNullOrEmpty(subNavTitle))
            SubConfig = GetConfig(subNavTitle);
      }

      private NavigConfig GetConfig(string name) {
         if(registredConfigs.ContainsKey(name))
            return registredConfigs[name];
         else
            return new NavigConfig(name);
      }

      public static void RegisterConfigurations(params NavigConfig[] configs) {
         foreach(var conf in configs)
            registredConfigs.Add(conf.Name, conf);
      }

   }
}
