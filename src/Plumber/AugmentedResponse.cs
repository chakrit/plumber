
using System.IO;

namespace Plumber
{
  public class AugmentedResponse : IResponse
  {
    private IResponse _resp;
    private Stream _stream;


    public int StatusCode
    {
      get { return _resp.StatusCode; }
      set { _resp.StatusCode = value; }
    }

    public string StatusMessage
    {
      get { return _resp.StatusMessage; }
      set { _resp.StatusMessage = value; }
    }


    public string ContentType
    {
      get { return _resp.ContentType; }
      set { _resp.ContentType = value; }
    }

    public Stream Stream
    {
      get { return _stream; }
    }

    public AugmentedResponse(IResponse response, Stream newStream)
    {
      Assert.ArgumentNotNull(() => response);
      Assert.ArgumentNotNull(() => newStream);

      _resp = response;
      _stream = newStream;
    }


    public void End()
    {
      _resp.End();
    }
  }
}
