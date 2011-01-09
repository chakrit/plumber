
using System.Diagnostics;

namespace Plumber.Framework
{
  // TODO: Add some more logging sink and options
  public static class Log
  {
    public static Pipe Message(string msg)
    {
      // can't use Pipes.Action due to Trace having a [Conditonal] attribute set
      return (ctx, next) =>
      {
        Trace.WriteLine(msg);
        next(ctx);
      };
    }

    public static Pipe Error(string msg)
    {
      // can't use Pipes.Action due to Trace having a [Conditonal] attribute set
      return (ctx, next) =>
      {
        Trace.TraceError(msg);
        next(ctx);
      };
    }
  }
}
