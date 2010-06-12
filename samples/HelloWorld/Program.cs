
using System.IO;

using Plumber;
using Plumber.Server;

namespace HelloWorld
{
  public class Program
  {
    internal static void Main() { new Program().Run(); }


    public void Run()
    {
      IServer server = new HttpListenerServer((req, resp) =>
      {
        var sw = new StreamWriter(resp.Stream);
        sw.Write("Hello World!");
        sw.Close();
      });

      server.Start();
    }
  }
}
