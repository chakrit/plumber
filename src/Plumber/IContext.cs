
namespace Plumber
{
  public interface IContext
  {
    IRequest Request { get; }
    IResponse Response { get; }
  }
}
