
namespace Plumber.Servers
{
  public partial class HttpListenerServer : ServerBase
  {
    public const string DefaultUrl = "http://localhost:80/";


    public HttpListenerServer(RequestHandler handler)
      : this(new[] { DefaultUrl }, handler) { }

    public HttpListenerServer(string host, int port, RequestHandler handler)
      : this(new[] { string.Format("http://{0}:{1}/", host, port) }, handler) { }

    public HttpListenerServer(string[] urls, RequestHandler handler)
      : base(urls, handler) { }
  }
}
