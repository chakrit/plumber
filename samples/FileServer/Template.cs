
using System.IO;
using System.Linq;

namespace FileServer
{
  public class Template
  {
    public string BasePath { get; private set; }

    public Template(string basePath)
    {
      BasePath = Path.GetFullPath(basePath);
    }


    public string RenderIndex(string currentPath, string[] entries)
    {
      // normalize all paths
      currentPath = Path
        .GetFullPath(currentPath)
        .Substring(BasePath.Length);

      var normEntries =
        from e in entries
        let norm = normalizePath(currentPath, e)
        select new {
          Original = e,
          Filename = Path.GetFileName(e),
          Norm = norm
        };

      // render template
      var template = @"
        <html>
          <head><title>{0}</title></head>
          <body>
            <h1>Index of `{0}`</h1>
            <ul>
              {1}
            </ul>
          </body>
        </html>".Trim();

      var liTemplate = @"<li><a href=""{0}"">{1}</a></li>";

      var listItems = string.Join("\r\n", normEntries
        .Select(entry => string.Format(liTemplate, entry.Norm, entry.Filename)));

      return string.Format(template, currentPath, listItems);
    }


    private string normalizePath(string currentPath, string entry)
    {
      entry = Path.Combine(currentPath, entry);
      entry = Path.Combine(BasePath, entry.Substring(1));
      entry = Path.GetFullPath(entry);

      var result = entry.Substring(BasePath.Length);

      return result.Length == 0 ? "/" : result;
    }
  }
}
