
using System.Web;

namespace Plumber.Servers.AspNet
{
  public class AspNetBridge : ServerBase
  {
    public AspNetBridge(RequestHandler handler) : base(null, handler) { }

    protected override void StartCore() { /* no-op */ }
    protected override void StopCore() { /* no-op */ }


    public void Invoke(HttpContext context)
    {
      // use asp.net's request and response pipeline
      Handler(
        new AspNetRequestWrapper(context.Request),
        new AspNetResponseWrapper(context.Response));
    }
  }
}
