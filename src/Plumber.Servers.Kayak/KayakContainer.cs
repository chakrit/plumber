
using System;
using System.Collections.Generic;
using System.Net;

using Kayak;

namespace Plumber
{
  public class KayakContainer : OwinContainerBase<KayakServer>
  {
    private IDictionary<KayakServer, IDisposable> _instances;

    public KayakContainer()
    {
      _instances = new Dictionary<KayakServer, IDisposable>();
    }


    protected override KayakServer BuildServerCore(string host, int port)
    {
      Assert.ArgumentSatisfy(() => host,
        h => string.IsNullOrEmpty(h) || h == "localhost",
        "Only support localhost, for now.");

      var endPoint = new IPEndPoint(IPAddress.Loopback, port);
      return new KayakServer(endPoint);
    }

    protected override void StartCore(KayakServer server, Owin.IApplication owinApp)
    {
      var instance = server.Invoke(owinApp);
      _instances.Add(server, instance);
    }

    protected override void StopCore(KayakServer server, Owin.IApplication owinApp)
    {
      _instances[server].Dispose();
    }
  }
}
