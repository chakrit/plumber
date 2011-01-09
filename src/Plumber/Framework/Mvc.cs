
using System.Linq;

namespace Plumber.Framework
{
  public static class Mvc
  {
    public static Pipe Controllers(params Controller[] controllers)
    {
      return Map.Urls(controllers
        .Aggregate(new UrlMappings(), (m, ctr) => m.Merge(ctr.Mappings)));
    }
  }
}
