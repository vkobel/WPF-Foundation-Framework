using System.ComponentModel;
using System.Text;

namespace FoundationData {
   public partial class Person : IDataErrorInfo {

      public string Error {
         get { throw new System.NotImplementedException(); }
      }

      string IDataErrorInfo.this[string columnName] {
         get {
            var result = new StringBuilder();

            if(string.IsNullOrEmpty(columnName) || columnName == "Firstname") {
               if(string.IsNullOrEmpty(Firstname))
                  result.Append("Firstname cannot be empty");

            } else if(string.IsNullOrEmpty(columnName) || columnName == "Lastname") {
               if(string.IsNullOrEmpty(Lastname))
                   result.Append("Lastname cannot be empty");
            }
            return result.ToString();
         }
      }

   }
}
