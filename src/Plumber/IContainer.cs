
using System.Collections.Generic;

using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  // TODO: Change host/port combination to something more friendly to
  //   non-http.sys-using servers...
  public interface IContainer
  {
    RequestHandler BuildRequestHandler(Pipe pipes, IServicesBroker services);
    IServer BuildServer(string host, int port, RequestHandler handler);

    IObjectsStore BuildStore();
    IServicesBroker BuildServicesBroker(IEnumerable<IService> services);

    IContext BuildContext(IRequest request, IResponse response,
      IObjectsStore store, IServicesBroker servicesBroker);
  }
}
