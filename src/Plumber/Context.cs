
using Plumber.Services;

namespace Plumber
{
  public class Context : IContext
  {
    public IRequest Request { get; private set; }
    public IResponse Response { get; private set; }

    public IObjectsStore Store { get; private set; }
    public IServicesBroker Services { get; private set; }


    public Context(IRequest request, IResponse response,
      IObjectsStore store, IServicesBroker services)
    {
      Request = request;
      Response = response;

      Store = store;
      Services = services;
    }

    // helper method for optionally replacing one or more items
    public Context(IContext reference, IRequest newRequest = null,
      IResponse newResponse = null, IObjectsStore newStore = null,
      IServicesBroker newServices = null)
    {
      Request = newRequest ?? reference.Request;
      Response = newResponse ?? reference.Response;

      Store = newStore ?? reference.Store;
      Services = newServices ?? reference.Services;
    }


    public T Get<T>()
    {
      if (Store.Contains<T>())
        return Store.Get<T>();

      if (Services.CanGet<T>(this)) {
        var obj = Services.Get<T>(this);
        Store.Set<T>(obj);

        return obj;
      }

      throw Exceptions.CannotProvideObj<T>();
    }

    public bool Contains<T>()
    {
      return Store.Contains<T>() || Services.CanGet<T>(this);
    }

    public void Set<T>(T value) { Store.Set<T>(value); }

    public void Remove<T>() { Store.Remove<T>(); }

  }
}
