
using System;

namespace Plumber.Services
{
  public abstract class ServiceBase<T> : IService<T>
  {
    public abstract string Name { get; }
    public Type Type { get { return typeof(T); } }

    public abstract bool CanGet(IContext ctx);
    public abstract T Get(IContext ctx);
  }
}
