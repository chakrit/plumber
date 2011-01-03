
using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  public static class Pipes
  {
    public static Pipe Identity = c => c;


    public static IServer Connect(string host, int port, Pipe pipes)
    {
      return Connect(new DefaultContainer(), host, port, pipes);
    }

    public static IServer Connect(this IContainer container, Pipe pipes)
    {
      return Connect(container, "localhost", 80, pipes);
    }

    public static IServer Connect(this IContainer container,
      string host, int port, Pipe pipes, params IService[] services)
    {
      var broker = container.BuildServicesBroker(services);
      var handler = container.BuildRequestHandler(pipes, broker);

      return container.BuildServer(host, port, handler);
    }
  }
}
