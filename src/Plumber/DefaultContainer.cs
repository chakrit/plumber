﻿
using System;
using System.Collections.Generic;

using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  public class DefaultContainer : IContainer
  {
    public virtual RequestHandler BuildRequestHandler(Pipe pipes,
      IServicesBroker servicesBroker)
    {
      Assert.ArgumentsNotNull(() => pipes, () => servicesBroker);

      return (request, response) => pipes(
        BuildContext(request, response, BuildStore(), servicesBroker),
        Pipes.End);
    }

    public virtual IServer BuildServer(string host, int port, RequestHandler handler)
    {
      Assert.ArgumentNotNull(() => handler);
      Assert.ArgumentSatisfy(() => host, s => !string.IsNullOrEmpty(s),
        "Host cannot be null or empty.");
      Assert.ArgumentSatisfy(() => port, p => 0 < p && p < 65536,
        "Port must be between 0 and 65536 exclusive.");

      return new HttpListenerServer(host, port, handler);
    }


    public virtual IObjectsStore BuildStore()
    {
      return new ObjectsStore();
    }

    public virtual IServicesBroker BuildServicesBroker(IEnumerable<IService> services)
    {
      return services == null ?
        (IServicesBroker)new EmptyServicesBroker() :
        new ServicesBroker(services);
    }


    public virtual IContext BuildContext(IRequest request, IResponse response,
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
