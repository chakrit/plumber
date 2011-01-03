
using System.IO;
using System.Web;

namespace Plumber.Servers.AspNet
{
  public class AspNetResponseWrapper : IResponse
  {
    private HttpResponse _aspnet;


    public string ContentType
    {
      get { return _aspnet.ContentType; }
      set { _aspnet.ContentType = value; }
    }

    public int StatusCode
    {
      get { return _aspnet.StatusCode; }
      set { _aspnet.StatusCode = value; }
    }

    public string StatusMessage
    {
      get { return _aspnet.StatusDescription; }
      set { _aspnet.StatusDescription = value; }
    }

    public Stream Stream { get { return _aspnet.OutputStream; } }


    public AspNetResponseWrapper(HttpResponse aspnetResponse)
    {
      _aspnet = aspnetResponse;
    }


    public void End()
    {
      _aspnet.End();
    }
  }
}
