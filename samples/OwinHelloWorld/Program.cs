
using Plumber;
using Plumber.Framework;
using Plumber.Servers;

namespace OwinHelloWorld
{
  public class Program
  {
    internal static void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      // uses the built-in HttpListener server via an OwinContainer
      // replace with proper delegates when using your own web server
      var container = new OwinContainer<MyOwinServer>(
        (host, port) => new MyOwinServer(host, port),
        (serv, app) => serv.Start(app),
        (serv, app) => serv.Stop());

      Pipes
        .Connect(container, "localhost", 80, Static.String("Hello via OWIN!"))
        .Start();
    }
  }
}
