
using System.Collections.Generic;
using System.Linq;

namespace Plumber.Framework
{
  public class UrlMappings : Dictionary<string, Continuable>
  {
    public UrlMappings() : base() { }

    public UrlMappings(IDictionary<string, Continuable> dict) :
      base(dict) { }


    public Continuable FindMapping(string prefix, Continuable ifNotFound = null)
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

    public Continuable Map()
    {
      return Framework.Map.Urls(this);
    }


    #region Fluent Interface

    public static UrlMappings New(string urlPrefix, Continuable cont)
    {
      return new UrlMappings().Add(urlPrefix, cont);
    }

    public static UrlMappings New(string urlPrefix, Pipe pipe)
    {
      return new UrlMappings().Add(urlPrefix, pipe.AsContinuable());
    }

    public new UrlMappings Add(string urlPrefix, Continuable cont)
    {
      base.Add(urlPrefix, cont);
      return this;
    }

    public UrlMappings Add(string urlPrefix, Pipe pipe)
    {
      base.Add(urlPrefix, pipe.AsContinuable());
      return this;
    }

    #endregion

  }
}
