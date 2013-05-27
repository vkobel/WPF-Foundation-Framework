
namespace FoundationData {
   public interface IIndexer<T> {
      object this[T index] { get; }
   }
}
