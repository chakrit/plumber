
using System.IO;

namespace Plumber
{
  public interface IRequest
  {
    string Method { get; }
    string Path { get; }

    Stream Stream { get; }
  }
}
