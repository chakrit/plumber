
using Owin;

using Plumber.Servers;

namespace Plumber
{
  public abstract class OwinContainerBase<TOwinServer> : DefaultContainer
    where TOwinServer : class
  {
    protected abstract TOwinServer BuildServerCore(string host, int port);
    protected abstract void StartCore(TOwinServer server, IApplication owinApp);
    protected abstract void StopCore(TOwinServer server, IApplication owinApp);


    public override IServer BuildServer(string host, int port, RequestHandler handler)
    {
      var server = BuildServerCore(host, port);

      OwinServerBridge bridge = null;
      return bridge = new OwinServerBridge(handler,
        () => StartCore(server, bridge),
        () => StopCore(server, bridge));
    }
  }
}
