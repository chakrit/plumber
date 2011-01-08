
using System.Threading.Tasks;

namespace Plumber
{
  // core stuff
  // the server just pumps out requests and response, it doesn't know about pipes
  public delegate void RequestHandler(IRequest request, IResponse response);
  public delegate IContext ContextBuilder(IRequest request, IResponse response);

  // a `Pipe` takes a context, process something or transform it, if desired
  // then pass it on (usually to the next pipe via continuations)
  // a `Pipe<T>` is a special pipe that also requires an additional input of type T
  // for utility purposes
  public delegate IContext Pipe(IContext context);
  public delegate IContext Pipe<T>(IContext context, T obj);

  // a `Continuable` is an unfinished pipe, it requires the "next" pipe to be passed in
  // once passed in, then, can it continue. The finished (return value) `Pipe`
  // is the product of executing the pipe and the `next` pipe together
  public delegate Pipe Continuable(Pipe next);

  // Special continuables, used when any extra values are
  // required as input or output or both
  public delegate Pipe Produce<T>(Pipe<T> next);
  public delegate Pipe<T> Filter<T>(Pipe<T> next);
  public delegate Pipe<T> Consume<T>(Pipe next);


  public static class DelegateHelpers
  {
    public static Continuable AsContinuable(this Pipe pipe)
    {
      return next => ctx => next(pipe(ctx));
    }

    public static Pipe AsPipe(this Continuable cont)
    {
      return cont(Pipes.Identity);
    }
  }
}
