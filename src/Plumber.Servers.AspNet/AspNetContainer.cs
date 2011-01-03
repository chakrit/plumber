
using System;
using System.Web;

using Plumber.Services;

namespace Plumber.Servers.AspNet
{
  public class AspNetContainer : DefaultContainer
  {
    public override IServer BuildServer(string host, int port, RequestHandler handler)
    {
      throw new NotSupportedException();
    }

    public override IObjectsStore BuildStore()
    {
      return new AspNetObjectsStore(HttpContext.Current.Items);
    }

    // TODO: Use ASP.NET's session services
  }
}
