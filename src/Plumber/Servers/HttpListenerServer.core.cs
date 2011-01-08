
using System;
using System.Net;
using System.Threading;

namespace Plumber.Servers
{
  public partial class HttpListenerServer
  {
    public const string RequestPumpThreadName = "FoolsRequestPump";
    public const string RequestProcessorThreadName = "FoolsRequestHandler";
    public const int ConcurrentPumpCount = 10;


    private HttpListener _listener;
    private Thread _pumpThread;

    private ManualResetEvent _stopSignal;
    private CountdownEvent _requestsCounter;
    private Semaphore _pumpCounter;


    protected override void StartCore()
    {
      // initialize the requests counter with a dud count
      // as to not set the countdown event immediately
      _requestsCounter = new CountdownEvent(1);

      _stopSignal = new ManualResetEvent(false);
      _pumpCounter = new Semaphore(ConcurrentPumpCount, ConcurrentPumpCount);

      // initialize an http listener
      _listener = new HttpListener();

      foreach (var url in Urls)
        _listener.Prefixes.Add(url);

      // initialize and start the request pump
      _pumpThread = new Thread(_ => requestPump()) {
        IsBackground = false,
        Name = RequestPumpThreadName,
        Priority = ThreadPriority.AboveNormal
      };

      _listener.Start();
      _pumpThread.Start();
    }

    protected override void StopCore()
    {
      // stop gracefully
      _listener.Stop();
      _stopSignal.Set();

      // wait for all requests to finish
      // signal once to remove the dud counter
      _requestsCounter.Signal();
      _requestsCounter.Wait();

      // wait for the request pump to stop
      _pumpThread.Join();

      _listener.Abort();
      _listener = null;

      // cleanup-safe by this point
      _stopSignal.Dispose();
      _requestsCounter.Dispose();
      _pumpCounter.Dispose();
    }


    private void requestPump()
    {
      var handles = new WaitHandle[] { _pumpCounter, _stopSignal };

      while (WaitHandle.WaitAny(handles) == 0) /* means we got a pumpCounter */ {

        _listener.BeginGetContext(ar =>
        {
          _pumpCounter.Release();
          _requestsCounter.AddCount();

          try {
            var context = _listener.EndGetContext(ar);
            ThreadPool.QueueUserWorkItem(processRequest, context);
          }
          catch (Exception e) {
          }
          finally {
            _requestsCounter.Signal();
          }

        }, null);

      }
    }

    private void processRequest(object obj)
    {
      var rawContext = (HttpListenerContext)obj;
      Thread.CurrentThread.Name = RequestProcessorThreadName +
        Thread.CurrentThread.ManagedThreadId.ToString();

      var request = new HttpListenerRequestWrapper(rawContext.Request);
      var response = new HttpListenerResponseWrapper(rawContext.Response);

      try {
        Handler(request, response);
        response.End();
      }
      catch (Exception ex) {
        ReportError(null, ex);
      }
    }
  }
}
