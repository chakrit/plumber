
using System.Collections.Generic;
using System.IO;
using System.Linq;

using IOwinResponse = Owin.IResponse;

namespace Plumber.Servers
{
  public class OwinResponse : IOwinResponse, IResponse
  {
    private IDictionary<string, IEnumerable<string>> _headers;

    private MemoryStream _stream;
    private byte[] _buffer;

    public OwinResponse()
    {
      _headers = new Dictionary<string, IEnumerable<string>>();
      _stream = new MemoryStream();
    }

    // _________________________________________________________________________
    // Plumber interface

    public string ContentType
    {
      get
      {
        return _headers.ContainsKey("Content-Type") ?
          _headers["Content-Type"].First() :
          null;
      }
      set { _headers["Content-Type"] = new[] { value }; }
    }

    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }

    public Stream Stream { get { return _stream; } }

    public void End()
    {
      _buffer = _stream.ToArray();
      _stream.Close();
      _stream.Dispose();
    }

    // _________________________________________________________________________
    // OWIN interface

    public string Status
    {
      get { return StatusCode.ToString() + " " + StatusMessage; }
    }

    public IDictionary<string, IEnumerable<string>> Headers
    {
      get { return _headers; }
    }

    public IEnumerable<object> GetBody()
    {
      if (_buffer == null)
        End(); // converts streams to buffer

      yield return _buffer;
    }

  }
}
