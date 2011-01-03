
using System;
using System.IO;

using IOwinRequest = Owin.IRequest;

namespace Plumber.Servers
{
  public class OwinRequestWrapper : IRequest
  {
    private IOwinRequest _owin;
    private Stream _requestStream;

    public OwinRequestWrapper(IOwinRequest owinRequest) { _owin = owinRequest; }


    public string Method { get { return _owin.Method; } }

    public string Path
    {
      get { return new Uri(_owin.Uri).AbsolutePath; }
    }

    public Stream Stream
    {
      get
      {
        return _requestStream ??
          (_requestStream = new OwinRequestStream(_owin));
      }
    }

  }
}
