
using Owin;

namespace Plumber
{
  public delegate TOwinServer OwinServerFactory<TOwinServer>(string host, int port)
    where TOwinServer : class;

  public delegate void OwinServerAction<TOwinServer>(TOwinServer server, IApplication app)
    where TOwinServer : class;
}
