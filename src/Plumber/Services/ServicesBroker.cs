
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Plumber.Services
{
  public class ServicesBroker : IServicesBroker
  {
    private IDictionary<Type, IService> _services;


    public ServicesBroker(IEnumerable<IService> services)
    {
      // because the list of services doesn't change during runtime
      // we can speed things up by providing a fast concurrent service lookup
      var dict = services.ToDictionary(s => s.Type, s => s);

      _services = new ConcurrentDictionary<Type, IService>(dict);
    }


    public T Get<T>(IContext context)
    {
      return ((IService<T>)_services[typeof(T)]).Get(context);
    }

    public bool CanGet<T>(IContext context)
    {
      var type = typeof(T);

      IService service;
      if (!_services.TryGetValue(type, out service))
        return false;

      return (service is IService<T>) &&
        ((IService<T>)service).CanGet(context);
    }


    IEnumerator<IService> IEnumerable<IService>.GetEnumerator()
    { return _services.Values.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator()
    { return _services.Values.GetEnumerator(); }
  }
}
