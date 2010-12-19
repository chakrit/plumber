
using System.Net;
using System.Linq;

using Owin;
using System.Threading;
using System;

namespace OwinHelloWorld
{
  public class MyOwinServer
  {
    private HttpListener _listener;
    private Thread _thread;

    public MyOwinServer(string host, int port)
    {
      _listener = new HttpListener();
      _listener.Prefixes.Add("http://" + host + ":" + port.ToString() + "/");
    }


    public void Start(IApplication owinApp)
    {
      _thread = new Thread(() =>
      {
        while (true) {
          var context = _listener.GetContext();
          owinApp.BeginInvoke(new MyOwinRequest(context.Request), ar =>
          {
            var resp = owinApp.EndInvoke(ar);
            if (resp.Headers.ContainsKey("Content-Type"))
              context.Response.ContentType = resp.Headers["Content-Type"].First();
            if (resp.Headers.ContainsKey("Content-Length"))
              context.Response.ContentLength64 = long.Parse(
                resp.Headers["Content-Length"].First());

            var body = resp.GetBody();
            foreach (var obj in body) {
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

          }, null);
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

