
using System.Diagnostics;

namespace Plumber.Framework
{
  // TODO: Add some more logging sink and options
  public static class Log
  {
    public static Pipe Message(string msg)
    { return Pipes.Action(Trace.WriteLine, msg, "PIPES"); }

    public static Pipe Error(string msg)
    { return Pipes.Action(Trace.TraceError, msg); }
  }
}
