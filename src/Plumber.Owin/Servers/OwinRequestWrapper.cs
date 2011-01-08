
using System;
using System.Collections.Generic;
using System.IO;

using OwinReader = System.Action<byte[], int, int, System.Action<int>, System.Action<System.Exception>>;

namespace Plumber.Servers
{
  public class OwinRequestWrapper : IRequest
  {
    private IDictionary<string, object> _owinEnv;
    private Stream _requestStream;

    public OwinRequestWrapper(IDictionary<string, object> owinEnv)
    {
      _owinEnv = owinEnv;
    }


    public string Method { get { return (string)_owinEnv["RequestMethod"]; } }

    public string Path
    {
      get
      {
        return new Uri((string)_owinEnv["RequestUri"], UriKind.Relative)
          .AbsolutePath;
      }
    }

    public Stream Stream
    {
      get
      {
        if (_requestStream != null)
          return _requestStream;

        return new OwinRequestStream((OwinReader)_owinEnv["RequestBody"]);
      }
    }

  }
}
