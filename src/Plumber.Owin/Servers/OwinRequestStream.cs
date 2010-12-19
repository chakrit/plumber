
using System;
using System.IO;

using IOwinRequest = Owin.IRequest;

namespace Plumber.Servers
{
  public class OwinRequestStream : Stream
  {
    private IOwinRequest _owin;

    public OwinRequestStream(IOwinRequest owinRequest) { _owin = owinRequest; }


    public override bool CanRead { get { return true; } }
    public override bool CanSeek { get { return false; } }
    public override bool CanWrite { get { return false; } }

    public override long Length { get { throw new NotSupportedException(); } }

    public override long Position
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }


    public override void Flush()
    {
      throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      var result = 0;
      var ar = _owin.BeginReadBody(buffer, offset, count,
        ar_ => result = _owin.EndReadBody(ar_), null);

      ar.AsyncWaitHandle.WaitOne();
      return result;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }
  }
}
