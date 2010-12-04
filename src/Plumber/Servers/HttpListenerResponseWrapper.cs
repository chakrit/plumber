
using System.IO;
using System.Net;

namespace Plumber.Servers
{
  public class HttpListenerResponseWrapper : IResponse
  {
    private HttpListenerResponse _response;


    public string ContentType
    {
      get { return _response.ContentType; }
      set { _response.ContentType = value; }
    }

    public int StatusCode
    {
      get { return _response.StatusCode; }
      set { _response.StatusCode = value; }
    }

    public string StatusMessage
    {
      get { return _response.StatusDescription; }
      set { _response.StatusDescription = value; }
    }


    public Stream Stream
    {
      get { return _response.OutputStream; }
    }


    public HttpListenerResponseWrapper(HttpListenerResponse response)
    { _response = response; }


    public void End()
    {
      _response.Close();
    }
  }
}
