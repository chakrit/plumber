
using System.IO;
using System.Net;

namespace Plumber.Server
{
  public class HttpListenerRequestWrapper : IRequest
  {
    public Stream Stream { get; private set; }


    public HttpListenerRequestWrapper(HttpListenerRequest request)
    {
      Stream = request.InputStream;
    }

  }
}
