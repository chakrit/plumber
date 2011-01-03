
using Plumber;
using Plumber.Framework;
using Plumber.Servers.AspNet;

namespace AspNetHelloWorld
{
  public class App : PlumberHttpHandlerBase
  {
    protected override Pipe GetPipes()
    {
      return Static.String("Hello World!");
    }
  }
}