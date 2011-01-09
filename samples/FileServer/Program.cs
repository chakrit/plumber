
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
      onFolder: displayIndex(notFound: HttpErrors.NotFound()),
      onFile: sendFileContent(notFound: HttpErrors.NotFound()));

      Pipes
        .Connect("localhost", 80, pipe)
        .Start();
    }


    private Pipe indexCheck(Pipe onFolder, Pipe onFile)
    {
      return (ctx, next) =>
      {
        var path = mapPath(ctx.Request.Path);
        (string.IsNullOrEmpty(path) || Directory.Exists(path) ? onFolder : onFile)
          (ctx, next);
      };
    }

    private Pipe displayIndex(Pipe notFound)
    {
      var specialFolders = new[] { ".", ".." };

      return (ctx, next) =>
      {
        // validate path
        var curPath = Path.Combine(_basePath, ctx.Request.Path.Substring(1));
        var fullPath = Path.GetFullPath(curPath);

        if (!Directory.Exists(curPath) || !fullPath.StartsWith(_basePath)) {
          notFound(ctx, next);
          return;
        }

        // generate list of files and folders to display in the index
        var entries = Directory
          .GetFiles(curPath)
          .Concat(Directory.GetDirectories(curPath))
          .Select(path => path.Substring(_basePath.Length));

        if (curPath != _basePath)
          entries = entries.Concat(specialFolders);

        // render the index page
        var html = _template.RenderIndex(curPath, entries.ToArray());
        Static.String(Mime.Text.Html, html)(ctx, next);
      };
    }

    private Pipe sendFileContent(Pipe notFound)
    {
      return (ctx, next) =>
      {
        var path = mapPath(ctx.Request.Path);

        (isSafePath(path) && File.Exists(path) ?
          Static.File(path) : notFound)(ctx, next);
      };
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
