
using System;
using System.Net;
using System.Threading;

using Plumber;
using System.Collections.Generic;

namespace OwinHelloWorld
{
  public class MyOwinServer
  {
    private string _host;
    private int _port;

    private HttpListener _listener;
    private Thread _thread;

    public MyOwinServer(string host, int port)
    {
      _host = host;
      _port = port;

      _listener = new HttpListener();
      _listener.Prefixes.Add("http://" + _host + ":" + _port.ToString() + "/");
    }


    public void Start(OwinDelegate owinApp)
    {
      _thread = new Thread(() =>
      {
        while (true) {
          var context = _listener.GetContext();

          // init owin stuff
          var headers = new Dictionary<string, IList<string>>();
          foreach (string key in context.Request.Headers) {
            var value = context.Request.Headers[key];

            if (headers.ContainsKey(key))
              headers[key].Add(value);
            else
              headers[key] = new List<string> { value };
          }

          Action<byte[], int, int, Action<int>, Action<Exception>> reader =
            (buffer, offset, count, callback, errback) =>
            {
              try {
                callback(context.Request.InputStream.Read(buffer, offset, count));
              }
              catch (Exception ex) {
                errback(ex);
              }
            };

          var env = new Dictionary<string, object> {
            { "RequestMethod", context.Request.HttpMethod },
            { "RequestUri", context.Request.Url.PathAndQuery },
            { "RequestHeaders", headers },
            { "RequestBody", reader },
            { "BaseUri", "/" },
            { "ServerName", _host },
            { "ServerPort", _port },
            { "UriScheme", "http" },
            { "RemoteEndPoint", context.Request.RemoteEndPoint },
            { "Version", "1.0" }
          };

          owinApp(env, (statusLine, outHeaders, writables) =>
          {
            // NOTE: This loop is enough for simple cases
            // but is actually a wrong impl
            foreach (var head in outHeaders)
              foreach (var value in head.Value)
                context.Response.Headers[head.Key] = value;

            foreach (var obj in writables) {
              // only 2 types of body is supported, for now
              // actually OWIN specs requires 4 types

              if (obj is byte[]) {
                var rawBuffer = (byte[])obj;
                context.Response.OutputStream.Write(rawBuffer, 0, rawBuffer.Length);
              }
              else if (obj is ArraySegment<byte>) {
                var segment = (ArraySegment<byte>)obj;
                context.Response.OutputStream.Write(segment.Array,
                  segment.Offset, segment.Count);
              }
            }

            context.Response.Close();

          }, ex => Console.WriteLine(ex.ToString()));
        }
      });

      _listener.Start();
      _thread.Start();
    }

    public void Stop()
    {
      _thread.Abort();
      _thread = null;

      _listener.Stop();
    }

  }
}

