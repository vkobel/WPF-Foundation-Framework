
namespace FoundationWPF.Navigation.Config {
   class RH : NavigConfig {
      public RH() : base("Ressources Humaines", 0, true, "Informations sur tous les employés", "") { }
   }
   class Emp : NavigConfig {
      public Emp() : base("Employés", 0, true, "Informations sur tous les employés", "") { }
   }
   class EmpDetails : NavigConfig {
      public EmpDetails() : base("Persons", 1, true, "Some good stuff", "") { }
   }
}
