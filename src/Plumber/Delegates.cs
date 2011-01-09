
using System.Threading.Tasks;
using System;

namespace Plumber
{
  // core stuff
  // the server just pumps out requests and response, it doesn't know about pipes
  public delegate void RequestHandler(IRequest request, IResponse response);

  // a `Pipe` takes a context, process something or transform it, if desired
  // then pass it on to the next pipe via the passed-in continuation
  // a `Pipe<T>` is a special pipe that also requires an additional input of type T
  // for utility purposes
  public delegate void Pipe(IContext context, Action<IContext> next);

  // a special pipe for passing stuff to/from the next/previous pipe
  public delegate void Produce<T>(IContext context, Action<IContext, T> next);
  public delegate void Consume<T>(IContext context, T arg, Action<IContext> next);
}
