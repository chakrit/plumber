﻿
using System;
using System.Collections.Generic;

namespace Plumber.Servers
{
  // Base server class to help with boiler plate stuffs
  public abstract class ServerBase : IServer
  {
    private bool _isServing;


    public ICollection<string> Urls { get; protected set; }
    public RequestHandler Handler { get; protected set; }

    public bool IsServing { get { return _isServing; } }


    public ServerBase(string[] urls, RequestHandler handler)
    {
      if (urls == null)
        urls = new string[] { };

      Urls = Array.AsReadOnly(urls);
      Handler = handler;
    }


    public void Start()
    {
      if (IsServing)
        throw new InvalidOperationException("Server already started.");

      StartCore();
      _isServing = true;
    }

    public void Stop()
    {
      if (!IsServing)
        throw new InvalidOperationException("Server has not started.");

      StopCore();
      _isServing = false;
    }


    protected abstract void StartCore();
    protected abstract void StopCore();

    protected void ReportError(string message, Exception exception)
    {
      // TODO: Properly handle errors.
      // TODO: Maybe convert to a ErrorHandler delegate?
      Console.WriteLine(exception);
    }


    public void Dispose()
    {
      if (_isServing) Stop();
    }
  }
}
