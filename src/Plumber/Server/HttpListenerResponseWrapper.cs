
using System.IO;
using System.Net;

namespace Plumber.Server
{
  public class HttpListenerResponseWrapper : IResponse
  {
    public Stream Stream { get; private set; }


    public HttpListenerResponseWrapper(HttpListenerResponse response)
    {
      Stream = response.OutputStream;
    }
  }
}
