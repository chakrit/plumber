
using System;
using System.Collections.Generic;

namespace Plumber
{
  public interface IServer : IDisposable
  {
    ICollection<string> Urls { get; }
    RequestHandler Handler { get; }

    bool IsServing { get; }

    void Start();
    void Stop();
  }
}
