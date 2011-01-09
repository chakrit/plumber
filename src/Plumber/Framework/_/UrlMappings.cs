
using System.Collections.Generic;
using System.Linq;

namespace Plumber.Framework
{
  public class UrlMappings : Dictionary<string, Pipe>
  {
    public UrlMappings() : base() { }

    public UrlMappings(IDictionary<string, Pipe> dict) :
      base(dict) { }


    public Pipe FindMapping(string prefix, Pipe ifNotFound = null)
    {
      var key = Keys.FirstOrDefault(k => k.StartsWith(prefix));
      return key == null ? ifNotFound : this[key];
    }

    public UrlMappings Merge(UrlMappings another)
    {
      foreach (var item in another)
        Add(item.Key, item.Value);

      return this;
    }

    public Pipe Map()
    {
      return Framework.Map.Urls(this);
    }


    // fluent interface
    public static UrlMappings New(string urlPrefix, Pipe cont)
    {
      return new UrlMappings().Add(urlPrefix, cont);
    }

    public new UrlMappings Add(string urlPrefix, Pipe pipe)
    {
      base.Add(urlPrefix, pipe);
      return this;
    }

  }
}
