
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;

namespace OwinHelloWorld
{
  public class MyOwinRequest : Owin.IRequest
  {
    private HttpListenerRequest _request;
    private IDictionary<string, object> _items;


    public string Method { get { return _request.HttpMethod; } }
    public string Uri { get { return _request.Url.PathAndQuery; } }

    public IDictionary<string, IEnumerable<string>> Headers
    {
      get
      {
        return _request.Headers.AllKeys
          .ToDictionary(k => k, k => Enumerable.Repeat(_request.Headers[k], 1));
      }
    }

    public IDictionary<string, object> Items
    {
      get { return _items ?? (_items = new Dictionary<string, object>()); }
    }


    public MyOwinRequest(HttpListenerRequest request)
    {
      _request = request;
    }


    public IAsyncResult BeginReadBody(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return _request.InputStream.BeginRead(buffer, offset, count, callback, state);
    }

    public int EndReadBody(IAsyncResult result)
    {
      return _request.InputStream.EndRead(result);
    }
  }
}
