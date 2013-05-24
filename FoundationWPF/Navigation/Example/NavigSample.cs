
namespace FoundationWPF.Navigation.Config {
   class RH : NavigConfig {
      public RH() : base("HR", 0, true, "Information about HR", "") { }
   }

   class Persons : NavigConfig {
      public Persons() : base("Persons", 1, true, "Persons collection", "") { }
   }

   class Hello : NavigConfig {
      public Hello() : base("Hello", 2, true, "Hello stuff", "") { }
   }
   class World : NavigConfig {
      public World() : base("World", 0, true, "Some good stuff", "") { }
   }
}
