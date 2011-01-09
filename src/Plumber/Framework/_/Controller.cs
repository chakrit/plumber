
using System;

namespace Plumber.Framework
{
  public abstract class Controller
  {
    private UrlMappings _mappings = new UrlMappings();

    public UrlMappings Mappings { get { return _mappings; } }


    public abstract void Initialize();


    protected void Get(string urlPrefix, Pipe cont)
    { mapCore(urlPrefix, HttpMethods.Get, cont); }

    protected void Post(string urlPrefix, Pipe cont)
    { mapCore(urlPrefix, HttpMethods.Post, cont); }

    protected void Put(string urlPrefix, Pipe cont)
    { mapCore(urlPrefix, HttpMethods.Put, cont); }

    protected void Delete(string urlPrefix, Pipe cont)
    { mapCore(urlPrefix, HttpMethods.Delete, cont); }


    // TODO: Eliminate these AsPipe/AsConitnuable switches... framework-wide
    private void mapCore(string urlPrefix, Func<Pipe, Pipe, Pipe> mapFunc, Pipe cont)
    {
      var err = HttpErrors.MethodNotAllowed();
      var notMethod = _mappings.FindMapping(urlPrefix, ifNotFound: err);

      _mappings[urlPrefix] = mapFunc(cont, notMethod);
    }
  }
}
