
using System;
using System.IO;

using OwinReader = System.Action<byte[], int, int, System.Action<int>, System.Action<System.Exception>>;

namespace Plumber.Servers
{
  public class OwinRequestStream : Stream
  {
    private OwinReader _owinReader;

    public OwinRequestStream(OwinReader owinReader) { _owinReader = owinReader; }


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
      // TODO: check buffer nulls and bounds
      Exception exception = null;
      var result = 0;

      var async = _owinReader.BeginInvoke(buffer, offset, count,
        n => result = n,
        ex => { throw ex; },
        ar => _owinReader.EndInvoke(ar),
        null);

      async.AsyncWaitHandle.WaitOne();
      if (exception != null)
        throw exception;

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
