
using System.Collections.Generic;
using System.Linq;

namespace Plumber.Framework
{
  public static class ContinuationDictionaryHelpers
  {
    public static ContinuableDict ToContDict(
      this IDictionary<string, Continuable> dict)
    {
      return new ContinuableDict(dict);
    }
  }

  public class ContinuableDict : Dictionary<string, Continuable>
  {
    public ContinuableDict() : base() { }

    public ContinuableDict(IDictionary<string, Continuable> dict) :
      base(dict) { }

    public static ContinuableDict New(string urlPrefix, Continuable cont)
    {
      return new ContinuableDict().Add(urlPrefix, cont);
    }

    public new ContinuableDict Add(string urlPrefix, Continuable cont)
    {
      base.Add(urlPrefix, cont);
      return this;
    }

    public ContinuableDict Add(string urlPrefix, Pipe pipe)
    {
      base.Add(urlPrefix, next => ctx => next(pipe(ctx)));
      return this;
    }


    public Continuable FindMapping(string prefix, Continuable ifNotFound = null)
    {
      var key = Keys.FirstOrDefault(k => k.StartsWith(prefix));
      return key == null ? ifNotFound : this[key];
    }
  }
}
