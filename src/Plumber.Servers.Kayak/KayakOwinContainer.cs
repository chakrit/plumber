
using System;
using System.Collections.Generic;

using Plumber.Services;
using Plumber.Servers;

namespace Plumber
{
  public class KayakOwinContainer : DefaultContainer
  {
    public override IServer BuildServer(string host, int port, RequestHandler handler)
    {
      // TODO: Implement host and port support
      if (!string.IsNullOrEmpty(host))
        throw new NotSupportedException(
          "Host and port param not supported for Kayak (yet). Please set it to null.");

      return new KayakServerWrapper(handler);
    }
  }
}
