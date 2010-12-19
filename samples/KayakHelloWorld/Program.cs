
using Plumber;
using Plumber.Framework;

namespace KayakHelloWorld
{
  public class Program
  {
    static internal void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      Pipes.Connect(new KayakOwinContainer(),
      host: null,
      port: 80,
      pipes: Static.String("Hello World!"))

      .Start();
    }

  }
}
