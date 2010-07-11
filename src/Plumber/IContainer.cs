
namespace Plumber
{
  public interface IContainer
  {
    RequestHandler BuildRequestHandler(Pipe pipes);
    IServer BuildServer(string host, int port, RequestHandler handler);

    IContext BuildContext(IRequest request, IResponse response);
  }
}
