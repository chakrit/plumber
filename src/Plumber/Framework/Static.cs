
using System.IO;

namespace Plumber.Framework
{
  public static class Static
  {
    public static Pipe String(string str)
    { return Static.String(next => ctx => next(ctx, str)); }

    public static Pipe String(string contentType, string str)
    { return Static.String(contentType, next => ctx => next(ctx, str)); }

    public static Pipe String(Produce<string> stringPipe)
    { return stringPipe(renderString); }

    public static Pipe String(string contentType, Produce<string> stringPipe)
    {
      return stringPipe((ctx, str) =>
      {
        ctx.Response.ContentType = contentType;
        return renderString(ctx, str);
      });
    }

    private static IContext renderString(IContext ctx, string str)
    {
      var sw = new StreamWriter(ctx.Response.Stream);
      sw.Write(str);
      sw.Close();

      return ctx;
    }


    public static Pipe File(string filename)
    {
      var mime = Mime.FromFilename(filename);
      return Static.File(mime, next => ctx => next(ctx, filename));
    }

    public static Pipe File(string contentType, string filename)
    {
      return Static.File(contentType, next => ctx => next(ctx, filename));
    }

    public static Pipe File(Produce<string> filenamePipe)
    {
      return filenamePipe((ctx, filename) =>
        renderFile(ctx, Mime.FromFilename(filename), filename));
    }

    public static Pipe File(string contentType, Produce<string> filenamePipe)
    {
      return filenamePipe((ctx, filename) =>
        renderFile(ctx, contentType, filename));
    }

    private static IContext renderFile(IContext ctx,
      string contentType, string filename)
    {
      ctx.Response.ContentType = contentType;

      var fs = System.IO.File.OpenRead(filename);
      fs.CopyTo(ctx.Response.Stream);
      fs.Close();

      ctx.Response.Stream.Close();
      return ctx;
    }
  }
}
