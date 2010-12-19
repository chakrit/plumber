
using Plumber;
using Plumber.Framework;

namespace KayakHelloWorld
{
  public class Program
  {
    static internal void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      Pipes
        .Connect(new KayakContainer(), "localhost", 80, Static.String("Hello World!"))
        .Start();
    }
  }
}
