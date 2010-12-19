
using System;
using System.Threading;

using IOwinResponse = Owin.IResponse;

namespace Plumber.Servers
{
  internal class OwinAsyncResult : IAsyncResult
  {
    private IAsyncResult _result;
    private IOwinResponse _response;


    public object AsyncState { get { return _result.AsyncState; } }
    public WaitHandle AsyncWaitHandle { get { return _result.AsyncWaitHandle; } }
    public bool CompletedSynchronously { get { return _result.CompletedSynchronously; } }
    public bool IsCompleted { get { return _result.IsCompleted; } }

    public IAsyncResult Inner { get { return _result; } }
    public IOwinResponse Response { get { return _response; } }

    public OwinAsyncResult(IAsyncResult inner, IOwinResponse
      response)
    {
      _result = inner;
      _response = response;
    }
  }
}
