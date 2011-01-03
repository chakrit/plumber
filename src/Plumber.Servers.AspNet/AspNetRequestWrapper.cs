
using System.IO;
using System.Web;

namespace Plumber.Servers.AspNet
{
  public class AspNetRequestWrapper : IRequest
  {
    private HttpRequest _aspnet;


    public string Method { get { return _aspnet.HttpMethod; } }
    public string Path { get { return _aspnet.Url.ToString(); } }

    public Stream Stream { get { return _aspnet.InputStream; } }

    public AspNetRequestWrapper(HttpRequest aspnetRequest)
    {
      _aspnet = aspnetRequest;
    }

  }
}
