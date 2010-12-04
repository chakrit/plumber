
using System.IO;

namespace Plumber
{
  public interface IResponse
  {
    string ContentType { get; set; }

    int StatusCode { get; set; }
    string StatusMessage { get; set; }

    Stream Stream { get; }


    void End();
  }
}
