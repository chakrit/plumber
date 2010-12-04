
using Plumber;
using Plumber.Framework;

namespace HelloWorld
{
  public class Program
  {
    static internal void Main()
    {
      new Program().Run();
    }


    public void Run ()
    {
      Pipes.Connect ("localhost", 80, Static.String ("Hello World!")).Start ();
    }
  }
}
