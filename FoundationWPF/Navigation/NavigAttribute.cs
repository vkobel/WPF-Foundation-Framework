using System;
using System.Collections.Generic;

namespace FoundationWPF.Navigation {

   /// <summary>
   /// Applicable to a ViewModel in order to signal to generate the appropriate navigation item
   /// </summary>
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
   public class NavigAttribute : Attribute {

      private static Dictionary<string, NavigConfig> registredConfigs = new Dictionary<string, NavigConfig>();

      public NavigConfig MainConfig { get; private set; }
      public NavigConfig SubConfig { get; private set; }

      public NavigAttribute(string mainNavTitle, string subNavTitle = "") {
         MainConfig = GetConfig(mainNavTitle);
         if(!string.IsNullOrEmpty(subNavTitle))
            SubConfig = GetConfig(subNavTitle);
      }

      /// <summary>
      /// Try to get the registred configuration (if it exists) otherwise it uses a default one.
      /// </summary>
      /// <param name="name">The name of the navigation item</param>
      /// <returns>A NavigConfig object containing the parameters for this item</returns>
      private NavigConfig GetConfig(string name) {
         if(registredConfigs.ContainsKey(name))
            return registredConfigs[name];
         else
            return new NavigConfig(name);
      }

      /// <summary>
      /// Register a new NavigConfig in the system
      /// </summary>
      /// <param name="configs">An array of NavigConfig to be used by the attribute</param>
      public static void RegisterConfigurations(params NavigConfig[] configs) {
         foreach(var conf in configs)
            registredConfigs.Add(conf.Name, conf);
      }

   }
}
