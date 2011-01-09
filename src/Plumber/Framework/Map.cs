
namespace Plumber.Framework
{
  public static class Map
  {
    public static Pipe Urls(UrlMappings mappings)
    { return Urls(mappings, HttpErrors.NotFound()); }

    public static Pipe Urls(UrlMappings mappings, Pipe on404)
    { return Custom(mappings, Pipes.Produce(ctx => ctx.Request.Path), on404); }


    public static Pipe Custom(UrlMappings mappings,
      Produce<string> pathFunc, Pipe on404)
    {
      return (c0, next) => pathFunc(c0, (ctx, path) =>
        mappings.FindMapping(path, ifNotFound: on404)(ctx, next));
    }
  }
}
