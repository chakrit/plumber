
using System;
using System.Collections.Generic;

namespace Plumber
{
  public partial class ObjectsStore : IObjectsStore
  {
    private IDictionary<Type, object> _dict;


    public ObjectsStore()
    {
      _dict = new Dictionary<Type, object>();
    }


    public T Get<T>()
    {
      var key = typeof(T);

      object result;
      if (!_dict.TryGetValue(key, out result))
        throw Exceptions.ObjKeyNotFound<T>();

      return (T)result;
    }

    public bool Contains<T>()
    {
      return _dict.ContainsKey(typeof(T));
    }

    public void Set<T>(T value)
    {
      _dict[typeof(T)] = value;
    }

    public void Remove<T>()
    {
      _dict.Remove(typeof(T));
    }
  }
}
