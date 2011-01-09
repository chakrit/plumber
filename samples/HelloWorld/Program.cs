
using Plumber;
using Plumber.Framework;

namespace HelloWorld
{
  public class Program
  {
    internal static void Main() { new Program().Run(); }


    public void Run()
    {
      Pipes.Connect(Static.String("Hello World!")).Start();
    }

  }
}
