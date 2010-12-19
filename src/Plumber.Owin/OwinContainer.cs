
using System;

using Plumber.Servers;

namespace Plumber
{
  public class OwinContainer<TOwinServer> : DefaultContainer
    where TOwinServer : class
  {
    private OwinServerFactory<TOwinServer> _factory;
    private OwinServerAction<TOwinServer> _onStart;
    private OwinServerAction<TOwinServer> _onStop;

    public OwinContainer(
      OwinServerFactory<TOwinServer> factory,
      OwinServerAction<TOwinServer> startCallback,
      OwinServerAction<TOwinServer> stopCallback)
    {
      _factory = factory;
      _onStart = startCallback;
      _onStop = stopCallback;
    }

    public override IServer BuildServer(string host, int port, RequestHandler handler)
    {
      var server = _factory(host, port);

      OwinServerBridge bridge = null;
      return bridge = new OwinServerBridge(handler,
        () => _onStart(server, bridge),
        () => _onStop(server, bridge));
    }
  }
}
