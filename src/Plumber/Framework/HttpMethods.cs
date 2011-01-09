
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plumber.Framework
{
  // TODO: Should use Continuables instead?
  public static class HttpMethods
  {
    public class MethodMap
    {
      public Pipe OnGet { get; set; }
      public Pipe OnPut { get; set; }
      public Pipe OnPost { get; set; }
      public Pipe OnDelete { get; set; }

      public Pipe OnHead { get; set; }
      public Pipe OnTrace { get; set; }
      public Pipe OnOptions { get; set; }
      public Pipe OnUnknownMethod { get; set; }

      public bool AutoHead { get; set; }
      public bool AutoTrace { get; set; }
      public bool AutoOptions { get; set; }
      public bool AutoUnknownMethod { get; set; }

      public MethodMap()
      {
        AutoHead = AutoTrace = AutoOptions = AutoUnknownMethod = true;
      }
    }


    public static Pipe Get(Pipe onGet, Pipe notGet)
    { return Custom("GET", onGet, notGet); }

    public static Pipe Put(Pipe onPut, Pipe notPut)
    { return Custom("PUT", onPut, notPut); }

    public static Pipe Post(Pipe onPost, Pipe notPost)
    { return Custom("POST", onPost, notPost); }

    public static Pipe Delete(Pipe onDelete, Pipe notDelete)
    { return Custom("DELETE", onDelete, notDelete); }

    public static Pipe Custom(string methodName, Pipe onMethod, Pipe notMethod)
    {
      return ctx => ctx.Request.Method.FastEquals(methodName) ?
        onMethod(ctx) :
        notMethod(ctx);
    }


    public static Pipe Head(Pipe onHead, Pipe notHead)
    {
      return Custom("HEAD", onHead, notHead);
    }

    public static Pipe Head2(Pipe onGet, Pipe notGetOrHead)
    {
      var notHead = Custom("GET", onGet, notGetOrHead);

      return Custom("HEAD", ctx =>
      {
        // consume the response body, whilst allowing HTTP headers to be set
        // TODO: Should end the response as soon as data starts to be written
        // to save wasted cycles.... not sure how yet though.
        var response = new AugmentedResponse(ctx.Response, new NullStream());

        return onGet(new Context(ctx, newResponse: response));
      }, notHead);
    }


    public static Pipe Trace(Pipe notTrace)
    {
      // TODO: Implement TRACE
      return Log.Message("TRACE is not yet implemented.");
    }

    public static Pipe Options(Pipe onOptions, Pipe notOptions)
    {
      // TODO: Implement OPTIONS
      return Log.Message("OPTIONS is not yet implemented.");
    }

    public static Pipe Options(string[] supportedMethods, Pipe notOptions)
    {
      // TODO: Implement OPTIONS
      return Log.Message("OPTIONS is not yet implemented.");
    }


    public static Pipe Map(MethodMap map)
    {
      return Map(map.OnGet, map.OnPut, map.OnPost, map.OnDelete,
        map.AutoHead, map.OnHead,
        map.AutoTrace, map.OnTrace,
        map.AutoOptions, map.OnOptions,
        map.AutoUnknownMethod, map.OnUnknownMethod);
    }

    public static Pipe Map(Pipe onGet = null, Pipe onPut = null,
      Pipe onPost = null, Pipe onDelete = null,
      bool autoHead = true, Pipe onHead = null,
      bool autoTrace = true, Pipe onTrace = null,
      bool autoOptions = true, Pipe onOptions = null,
      bool autoUnknownMethod = true, Pipe onUnknownMethod = null)
    {
      // insert methods with auto = true
      if (autoUnknownMethod)
        onUnknownMethod = HttpErrors.MethodNotAllowed();
      else if (onUnknownMethod == null)
        onUnknownMethod = Pipes.Identity;

      if (autoTrace)
        onTrace = Trace(notTrace: onUnknownMethod);

      if (autoHead)
        onHead = Head2(onGet: onGet, notGetOrHead: onUnknownMethod);

      var map = new Dictionary<string, Pipe> {
        { "GET", onGet },
        { "POST", onPost },
        { "PUT", onPut },
        { "DELETE", onDelete },
        { "TRACE", onTrace },
        { "HEAD", onHead },
        { "OPTIONS", onOptions }
      };

      // OPTIONS requires knowledge of other methods
      if (autoOptions) {
        var supportedMethods = map
          .Where(x => x.Value != null)
          .Select(x => x.Key)
          .ToArray();

        map["OPTIONS"] = Options(supportedMethods, notOptions: onUnknownMethod);
      }

      return ctx => map.ContainsKey(ctx.Request.Method) ?
        map[ctx.Request.Method](ctx) :
        onUnknownMethod(ctx);
    }
  }
}
