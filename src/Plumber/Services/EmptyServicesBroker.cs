
using System.Collections;
using System.Collections.Generic;

namespace Plumber.Services
{
  public class EmptyServicesBroker : IServicesBroker
  {
    public T Get<T>(IContext context) { return default(T); }
    public bool CanGet<T>(IContext context) { return false; }

    public IEnumerator<IService> GetEnumerator() { yield break; }
    IEnumerator IEnumerable.GetEnumerator() { yield break; }
  }
}
