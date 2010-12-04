
using System.IO;

using NetMime = System.Net.Mime.MediaTypeNames;

namespace Plumber
{
  public static class Mime
  {
    public static class Text
    {
      public const string Html = NetMime.Text.Html;
      public const string Xml = NetMime.Text.Xml;
      public const string Plain = NetMime.Text.Plain;
      public const string Rich = NetMime.Text.RichText;
      public const string Css = "text/css";
      public const string JavaScript = "text/javascript";
    }

    public static class App
    {
      public const string Json = "application/json";
      public const string OctetStream = NetMime.Application.Octet;
      public const string Pdf = NetMime.Application.Pdf;
      public const string Soap = NetMime.Application.Soap;
      public const string Zip = NetMime.Application.Zip;
    }

    public static class Image
    {
      public const string Png = "image/png";
      public const string Jpeg = NetMime.Image.Jpeg;
      public const string Gif = NetMime.Image.Gif;
      public const string Tiff = NetMime.Image.Tiff;
      public const string Bmp = "image/bmp";
      public const string Ico = "image/vnd.microsoft.icon"; // per IANA records
    }


    public static string FromFilename(string filename)
    { return FromExtension(Path.GetExtension(filename)); }

    public static string FromExtension(string fileExt)
    {
      fileExt = fileExt.ToLower();
      if (fileExt.StartsWith("."))
        fileExt = fileExt.Substring(1);

      switch (fileExt.ToLower()) {
        case "htm":
        case "html":
        case "xhtml":
        case "asp":
        case "aspx":
        case "php":
          return Text.Html;

        case "jpeg":
        case "jpg":
          return Image.Jpeg;

        case "gif":
          return Image.Gif;
        case "png":
          return Image.Png;
        case "bmp":
          return Image.Bmp;
        case "ico":
          return Image.Ico;
        case "tiff":
          return Image.Tiff;

        case "xml":
          return Text.Html;
        case "txt":
          return Text.Plain;
        case "css":
          return Text.Css;
        case "js":
          return Text.JavaScript;

        case "pdf":
          return App.Pdf;
        case "zip":
          return App.Zip;
        case "json":
          return App.Json;

        case "exe":
        case "msi":
        case "dll":
        default:
          return App.OctetStream;

      }
    }
  }
}
