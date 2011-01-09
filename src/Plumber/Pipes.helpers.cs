
using System;

namespace Plumber
{
  public static partial class Pipes
  {
    public static Pipe Identity = (ctx, next) => next(ctx);
    public static Action<IContext> End = ctx => { /* consumed */ };


    public static Pipe Join(Pipe a, Pipe b)
    {
      return (c0, next) => a(c0, c1 => b(c1, next));
    }

    public static Pipe Join(Pipe a, Pipe b, Pipe c)
    {
      return (c0, next) => a(c0, c1 => b(c1, c2 => c(c2, next)));
    }

    public static Produce<T> Produce<T>(T value)
    {
      return (ctx, next) => next(ctx, value);
    }

    public static Produce<T> Produce<T>(Func<IContext, T> func)
    {
      return (ctx, next) => next(ctx, func(ctx));
    }

  }
}
