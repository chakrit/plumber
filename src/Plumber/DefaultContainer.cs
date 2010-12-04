
using System.Collections.Generic;

using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  internal class DefaultContainer : IContainer
  {
    public RequestHandler BuildRequestHandler(Pipe pipes, IServicesBroker servicesBroker)
    {
      Assert.ArgumentsNotNull(() => pipes, () => servicesBroker);

      return (req, resp) =>
        pipes(BuildContext(req, resp, BuildStore(), servicesBroker));
    }

    public IServer BuildServer(string host, int port, RequestHandler handler)
    {
      Assert.ArgumentNotNull(() => handler);
      Assert.ArgumentSatisfy(() => host, s => !string.IsNullOrEmpty(s),
        "Host cannot be null or empty.");
      Assert.ArgumentSatisfy(() => port, p => 0 < p && p < 65536,
        "Port must be between 0 and 65536 exclusive.");

      return new HttpListenerServer(host, port, handler);
    }


    public IObjectsStore BuildStore()
    {
      return new ObjectsStore();
    }

    public IServicesBroker BuildServicesBroker(IEnumerable<IService> services)
    {
      Assert.ArgumentNotNull(() => services);

      return new ServicesBroker(services);
    }


    public IContext BuildContext(IRequest request, IResponse response,
      IObjectsStore store, IServicesBroker servicesBroker)
    {
      Assert.ArgumentsNotNull(
        () => request,
        () => response,
        () => store,
        () => servicesBroker);

      return new Context(request, response, store, servicesBroker);
    }
  }
}
