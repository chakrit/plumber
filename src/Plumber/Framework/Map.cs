
using System.Linq;

using ContDict = System.Collections.Generic.IDictionary<string, Plumber.Continuable>;

namespace Plumber.Framework
{
  public static class Map
  {
    public static Continuable Urls(UrlMappings mappings)
    { return Urls(mappings, HttpErrors.NotFound()); }

    public static Continuable Urls(UrlMappings mappings, Pipe on404)
    { return Urls(mappings, on404.AsContinuable()); }

    public static Continuable Urls(UrlMappings mappings, Continuable on404)
    { return Custom(mappings, next => ctx => next(ctx, ctx.Request.Path), on404); }


    public static Continuable Custom(UrlMappings mappings,
      Produce<string> pathFunc, Continuable on404)
    {
      return next => ctx => pathFunc((ctx_, path) =>
        mappings.FindMapping(path, ifNotFound: on404)(next)(ctx))(ctx);
    }
  }
}
