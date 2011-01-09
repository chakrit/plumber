
using System;
using System.IO;

namespace Plumber.Framework
{
  public static class Static
  {
    public static Pipe String(string str)
    { return String((ctx, next) => next(ctx, str)); }

    public static Pipe String(string contentType, string str)
    { return String(contentType, (ctx, next) => next(ctx, str)); }

    public static Pipe String(Produce<string> stringPipe)
    { return String(Mime.Text.Plain, stringPipe); }

    public static Pipe String(string contentType, Produce<string> stringPipe)
    {
      return (c0, next) => stringPipe(c0, (ctx, str) =>
      {
        ctx.Response.ContentType = contentType;

        var sw = new StreamWriter(ctx.Response.Stream);
        sw.Write(str);
        sw.Close();

        next(ctx);
      });
    }


    public static Pipe File(string filename)
    {
      var mime = Mime.FromFilename(filename);
      return Static.File(mime, Pipes.Produce(filename));
    }

    public static Pipe File(string contentType, string filename)
    {
      return Static.File(contentType, Pipes.Produce(filename));
    }

    public static Pipe File(Produce<string> filenamePipe)
    {
      return (c0, next) => filenamePipe(c0, (ctx, filename) =>
        renderFile(ctx, Mime.FromFilename(filename), filename, next));
    }

    public static Pipe File(string contentType, Produce<string> filenamePipe)
    {
      return (c0, next) => filenamePipe(c0, (ctx, filename) =>
        renderFile(ctx, contentType, filename, next));
    }

    private static void renderFile(IContext ctx,
      string contentType, string filename, Action<IContext> next)
    {
      ctx.Response.ContentType = contentType;

      var fs = System.IO.File.OpenRead(filename);
      fs.CopyTo(ctx.Response.Stream);
      fs.Close();

      ctx.Response.Stream.Close();
      next(ctx);
    }
  }
}
