using GalaSoft.MvvmLight;
using System.Collections.Generic;
using FoundationWPF.ViewModel;

namespace FoundationWPF.Navigation {

   public class NavigConfig {
      public string Name { get; set; }
      public int Position { get; set; }
      public bool Enabled { get; set; }
      public string Description { get; set; }
      public string ImgPath { get; set; }
      
      public List<NavigConfig> SubConfig { get; set; }
      public ViewModelBase VM { get; set; }

      public NavigConfig(string name, int pos = 9999, bool enabled = true, string desc = "", string imgPath = "") {
         Name = name;
         Position = pos;
         Enabled = enabled;
         Description = desc;
         ImgPath = imgPath;
         SubConfig = new List<NavigConfig>();
      }
   }
}