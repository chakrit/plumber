
using System;
using System.IO;
using System.Linq;

using Plumber;
using Plumber.Framework;

namespace FileServer
{
  public class Program
  {
    internal static void Main() { new Program().Run(); }


    private string _basePath;
    private Template _template;

    public Program()
    {
      _basePath = Path.Combine(Environment.CurrentDirectory, "Content");
      _template = new Template(_basePath);
    }


    public void Run()
    {
      var pipe = indexCheck(
      onFolder: displayIndex(notFound: notFound()),
      onFile: sendFileContent(notFound: notFound()));

      Pipes
        .Connect("localhost", 80, pipe)
        .Start();
    }


    private Pipe indexCheck(Pipe onFolder, Pipe onFile)
    {
      return ctx =>
      {
        var path = mapPath(ctx.Request.Path);

        if (string.IsNullOrEmpty(path) ||
          Directory.Exists(path))
          return onFolder(ctx);

        return onFile(ctx);
      };
    }

    private Pipe notFound()
    {
      return ctx =>
      {
        var resp = ctx.Response;
        resp.StatusCode = 404;
        resp.StatusMessage = "NotFound";
        resp.Stream.Close();

        return ctx;
      };
    }

    private Pipe displayIndex(Pipe notFound)
    {
      var specialFolders = new[] { ".", ".." };

      return Static.String(Mime.Text.Html, (Pipe<string> next) => ctx =>
      {
        var curPath = Path
          .Combine(_basePath, ctx.Request.Path.Substring(1));

        // ensure valid path
        if (!Directory.Exists(curPath) ||
          !Path.GetFullPath(curPath).StartsWith(_basePath))
          return notFound(ctx);

        // enumerate files in the specified folder
        var entries = Directory.GetFiles(curPath)
          .Concat(Directory.GetDirectories(curPath))
          .Select(path => path.Substring(_basePath.Length));

        if (curPath != _basePath)
          entries = entries.Concat(specialFolders);

        var html = _template
          .RenderIndex(curPath, entries.ToArray());

        return next(ctx, html);
      });
    }

    private Pipe sendFileContent(Pipe notFound)
    {
      return Static.File((Pipe<string> next) => ctx =>
      {
        var path = mapPath(ctx.Request.Path);

        return isSafePath(path) && File.Exists(path) ?
          next(ctx, path) :
          notFound(ctx);
      });
    }


    private string mapPath(string requestPath)
    {
      if (!string.IsNullOrEmpty(requestPath))
        requestPath = requestPath.Substring(1); // removes preceding slash

      return Path.GetFullPath(Path.Combine(_basePath, requestPath));
    }

    private bool isSafePath(string path)
    {
      return Path.GetFullPath(path).StartsWith(_basePath);
    }
  }
}
