
using System;
using System.Collections.Generic;
using System.Net;

using Kayak;

namespace Plumber
{
  public class KayakContainer : OwinContainerBase<IKayakServer>
  {
    private IDictionary<IKayakServer, IDisposable> _instances;

    public KayakContainer()
    {
      _instances = new Dictionary<IKayakServer, IDisposable>();
    }


    protected override IKayakServer BuildServerCore(string host, int port)
    {
      Assert.ArgumentSatisfy(() => host,
        h => string.IsNullOrEmpty(h) || h == "localhost",
        "Only support localhost, for now.");

      var endPoint = new IPEndPoint(IPAddress.Loopback, port);
      return new DotNetServer(endPoint);
    }

    protected override void StartCore(IKayakServer server, Owin.IApplication owinApp)
    {
      var instance = server.Start();
      server.Host((env, respond, error) => respond(Tuple

      _instances.Add(server, instance);
    }

    protected override void StopCore(IKayakServer server, Owin.IApplication owinApp)
    {
      _instances[server].Dispose();
    }
  }
}
