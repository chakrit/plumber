
using System.Collections;

namespace Plumber.Servers.AspNet
{
  public class AspNetObjectsStore : IObjectsStore
  {
    private IDictionary _aspnet;

    public AspNetObjectsStore(IDictionary aspnetItems)
    {
      _aspnet = aspnetItems;
    }


    public T Get<T>() { return (T)_aspnet[typeof(T)]; }
    public void Set<T>(T value) { _aspnet[typeof(T)] = value; }
    public void Remove<T>() { _aspnet.Remove(typeof(T)); }

    public bool Contains<T>() { return _aspnet.Contains(typeof(T)); }
  }
}
