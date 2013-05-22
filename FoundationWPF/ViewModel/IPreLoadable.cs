
namespace FoundationWPF.ViewModel {
   
   interface IPreLoadable {

      /// <summary>
      /// Determines if the PreLoading is needed. Default is true. Overridding it and set it to false to disable PreLoading
      /// for very short loading tasks.
      /// </summary>
      bool IsPreLoadNeeded { get; set; }

      /// <summary>
      /// Indicates if there's a loading currently happening on the object
      /// </summary>
      bool IsCurrentlyLoading { get; set; }

      /// <summary>
      /// Put all the loading logic in this method
      /// </summary>
      void PreLoad();
   }
}
