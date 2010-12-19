
using System;
using System.Net;

using Kayak;

namespace Plumber.Servers
{
  public class KayakServerWrapper : ServerBase
  {
    private KayakServer _server;
    private IDisposable _runningInstance;

    public KayakServerWrapper(RequestHandler handler) :
      base(new[] { "localhost" }, handler) { }


    protected override void StartCore()
    {
      _server = new KayakServer();
      _server.Invoke(
    }

    protected override void StopCore()
    {
      _runningInstance.Dispose();
    }
  }
}
