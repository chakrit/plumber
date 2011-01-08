
using System;
using System.Collections.Generic;

namespace Plumber
{
  public delegate TOwinServer OwinServerFactory<TOwinServer>(string host, int port)
    where TOwinServer : class;

  public delegate void OwinServerAction<TOwinServer>(
    TOwinServer server, OwinDelegate owinApp)
    where TOwinServer : class;

  public delegate void OwinDelegate(
    IDictionary<string, object> env,
    Action<string, IDictionary<string, IList<string>>, IEnumerable<object>> respond,
    Action<Exception> err);
}
