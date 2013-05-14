using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobiWPFFramework.Navigation {
   public static class NavigConfigLoader {

      private static Dictionary<string, NavigConfig> registredConfigs = new Dictionary<string, NavigConfig>();

      // Get the predefined NavigConfig or a default one
      public static NavigConfig GetConfig(string name) {
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
