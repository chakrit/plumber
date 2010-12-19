
using System;

using Owin;

using Plumber.Servers;

namespace Plumber
{
  public delegate TOwinServer OwinServerFactory<TOwinServer>(string host, int port)
    where TOwinServer : class;

  public delegate void OwinServerAction<TOwinServer>(TOwinServer server, IApplication app)
    where TOwinServer : class;

  public class OwinContainer<TOwinServer> : OwinContainerBase<TOwinServer>
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


    protected override TOwinServer BuildServerCore(string host, int port)
    {
      return _factory(host, port);
    }

    protected override void StartCore(TOwinServer server, IApplication owinApp)
    {
      _onStart(server, owinApp);
    }

    protected override void StopCore(TOwinServer server, IApplication owinApp)
    {
      _onStop(server, owinApp);
    }
  }
}
