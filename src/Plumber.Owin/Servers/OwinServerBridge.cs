
using System;

using Plumber.Servers;

using IOwinApp = Owin.IApplication;
using IOwinRequest = Owin.IRequest;
using IOwinResponse = Owin.IResponse;

namespace Plumber.Servers
{
  public class OwinServerBridge : ServerBase, IOwinApp
  {
    private RequestHandler _handler;
    private Action _onStart;
    private Action _onStop;

    public OwinServerBridge(RequestHandler handler,
      Action startCallback,
      Action stopCallback) :
      base(new[] { "localhost:80" }, handler)
    {
      Assert.ArgumentNotNull(() => startCallback);
      Assert.ArgumentNotNull(() => stopCallback);

      _handler = handler;
      _onStart = startCallback;
      _onStop = stopCallback;
    }


    protected override void StartCore() { _onStart(); }
    protected override void StopCore() { _onStop(); }


    public IAsyncResult BeginInvoke(IOwinRequest request, AsyncCallback callback, object state)
    {
      var req = new OwinRequestWrapper(request);
      var resp = new OwinResponse();

      return _handler.BeginInvoke(req, resp,
        ar => callback(new OwinAsyncResult(ar, resp)), state);
    }

    public IOwinResponse EndInvoke(IAsyncResult result)
    {
      var ar = (OwinAsyncResult)result;
      _handler.EndInvoke(ar.Inner);

      return ar.Response;
    }
  }
}
