
using System;
using System.Collections.Generic;
using System.Web;

using Plumber.Services;

namespace Plumber.Servers.AspNet
{
  public abstract class PlumberHttpHandlerBase : IHttpHandler
  {
    private IContainer _container = new AspNetContainer();
    private AspNetBridge _bridge;


    public IContainer Container
    {
      get { return _container; }
      set
      {
        Assert.IsTrue(value != null,
          () => new ArgumentNullException("Container cannot be null."));

        _container = value;
      }
    }

    public bool IsReusable
    {
      get { return true; }
    }


    protected abstract Pipe GetPipes();

    protected virtual IEnumerable<IService> GetServices() { yield break; }


    public void ProcessRequest(HttpContext aspnetContext)
    {
      if (_bridge == null)
        _bridge = new AspNetBridge(
          _container.BuildRequestHandler(GetPipes(),
          _container.BuildServicesBroker(GetServices())));

      _bridge.Invoke(aspnetContext);
    }
  }
}
