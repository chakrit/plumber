
using System;

namespace Plumber.Services
{
  public interface IService
  {
    string Name { get; }
    Type Type { get; }
  }

  public interface IService<T> : IService
  {
    bool CanGet(IContext ctx);
    T Get(IContext ctx);
  }
}
