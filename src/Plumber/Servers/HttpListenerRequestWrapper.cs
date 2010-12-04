
using System.IO;
using System.Net;

namespace Plumber.Servers
{
  public class HttpListenerRequestWrapper : IRequest
  {
    private HttpListenerRequest _request;


    public string Method { get { return _request.HttpMethod; } }
    public string Path { get { return _request.Url.AbsolutePath; } }

    public Stream Stream { get { return _request.InputStream; } }


    public HttpListenerRequestWrapper(HttpListenerRequest request)
    { _request = request; }
  }
}
