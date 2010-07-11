
using Plumber.Server;

namespace Plumber
{
  internal class DefaultContainer : IContainer
  {
    public RequestHandler BuildRequestHandler(Pipe pipes)
    {
      return (req, resp) => pipes(BuildContext(req, resp));
    }

    public IServer BuildServer(string host, int port, RequestHandler handler)
    {
      return new HttpListenerServer(host, port, handler);
    }

    public IContext BuildContext(IRequest request, IResponse response)
    {
      return new Context(request, response);
    }
  }
}
