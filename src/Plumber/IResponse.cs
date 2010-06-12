
using System.IO;

namespace Plumber
{
  public interface IResponse
  {
    Stream Stream { get; }
  }
}
