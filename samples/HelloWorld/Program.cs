
using System.IO;

using Plumber;

namespace HelloWorld
{
  public class Program
  {
    internal static void Main() { new Program().Run(); }


    public void Run()
    {
      var server = Pipes.Connect("localhost", 80, ctx =>
      {
        var sw = new StreamWriter(ctx.Response.Stream);
        sw.Write("Hello, World!");
        sw.Close();

        return ctx;
      });

      server.Start();
    }
  }
}
