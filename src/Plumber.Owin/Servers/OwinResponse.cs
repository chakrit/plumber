
using System.Collections.Generic;
using System.IO;
using System.Linq;

using OwinCallback = System.Action<string, System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<string>>, System.Collections.Generic.IEnumerable<object>>;

namespace Plumber.Servers
{
  public class OwinResponse : IResponse
  {
    private OwinCallback _owinRespond;

    private IDictionary<string, IList<string>> _headers;
    private MemoryStream _stream;


    public string ContentType
    {
      get
      {
        return _headers.ContainsKey("Content-Type") ?
          _headers["Content-Type"].Last() :
          null;
      }
      set { _headers["Content-Type"] = new[] { value }.ToList(); }
    }

    // TODO: Validate these properties
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }

    public Stream Stream { get { return _stream; } }

    public OwinResponse(OwinCallback owinRespond)
    {
      _owinRespond = owinRespond;

      _headers = new Dictionary<string, IList<string>>();
      _stream = new MemoryStream();
    }


    public void End()
    {
      // invokes callback to owin interface
      var buffer = _stream.ToArray();
      _stream.Close();
      _stream.Dispose();

      _owinRespond(
        StatusCode.ToString() + " " + StatusMessage,
        _headers,
        new[] { buffer });
    }

  }
}
