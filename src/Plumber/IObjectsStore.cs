
namespace Plumber
{
  public interface IObjectsStore
  {
    T Get<T>();
    void Set<T>(T value);

    bool Contains<T>();
    void Remove<T>();
  }
}
