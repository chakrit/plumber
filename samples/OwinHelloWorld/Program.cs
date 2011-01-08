
using Plumber;
using Plumber.Framework;

namespace OwinHelloWorld
{
  public class Program
  {
    internal static void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      // use our own custom OWIN-compatible server to host the pipes
      new MyOwinServer("localhost", 80)
        .Start(Pipes.Connect(Static.String("Hello via OWIN!")).AsOwinApp());
    }
  }
}
