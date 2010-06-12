
namespace Plumber
{
  public class Context : IContext
  {
    public IRequest Request { get; private set; }
    public IResponse Response { get; private set; }


    public Context(IRequest request, IResponse response)
    {
      Request = request;
      Response = response;
    }

  }
}
