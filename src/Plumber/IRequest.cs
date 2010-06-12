
using System.IO;

namespace Plumber
{
  public interface IRequest
  {
    Stream Stream { get; }
  }
}
