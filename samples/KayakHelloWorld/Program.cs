
using System;
using System.Net;

using Kayak;

using Plumber;
using Plumber.Framework;

namespace KayakHelloWorld
{
  public class Program
  {
    static internal void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      var server = new DotNetServer(new IPEndPoint(IPAddress.Loopback, 80));
      var pipes = Pipes
        .Connect(Static.String("Hello, World!"));

      var instance = server.Start();
      server.Host(new OwinApplication(pipes.AsOwinApp()));

      Console.WriteLine("Listening...");
      Console.ReadKey();

      instance.Dispose();
    }
  }
}
