
using System.Collections.Generic;

namespace Plumber.Services
{
  public interface IServicesBroker : IEnumerable<IService>
  {
    T Get<T>(IContext context);
    bool CanGet<T>(IContext context);
  }
}
