
using System;

using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  public static class OwinExtensions
  {
    public static OwinDelegate AsOwinApp(this IServer server)
    { return AsOwinApp(server.Handler); }

    public static OwinDelegate AsOwinApp(this Continuable cont)
    { return AsOwinApp(cont(Pipes.Identity)); }

    public static OwinDelegate AsOwinApp(this Pipe pipe)
    {
      var container = new DefaultContainer();
      var handler = container.BuildRequestHandler(pipe,
        container.BuildServicesBroker(null));

      return AsOwinApp(handler);
    }

    public static OwinDelegate AsOwinApp(this RequestHandler handler)
    {
      return (env, respond, err) =>
      {
        var req = new OwinRequestWrapper(env);
        var resp = new OwinResponse(respond);

        try {
          handler(req, resp);
          resp.End();
        }
        catch (Exception ex) { err(ex); }
      };
    }
  }
}
