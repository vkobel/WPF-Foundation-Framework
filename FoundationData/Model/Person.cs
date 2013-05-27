
namespace FoundationData {
   public partial class Person : IIndexer<string> {

      // ***********************************
      // ***********************************
      //      IMPLEMENT IDataErrorInfo
      //    Do not persist or raisePropChanged
      //      if there are any errors
      // ***********************************
      // ***********************************

      object IIndexer<string>.this[string colname] {
         get {
            string result = null;
            switch(colname) {
               case "Firstname":
                  if(string.IsNullOrEmpty(Firstname))
                     result = "Firstname cannot be empty";
                  break;
               case "Lastname":
                  if(string.IsNullOrEmpty(Lastname))
                     result = "Lastname cannot be empty";
                  break;
            }
            return result;
         }
      }
   }
}
