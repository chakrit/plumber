
using Plumber.Servers;
using Plumber.Services;

namespace Plumber
{
  public static partial class Pipes
  {
    public const string DefaultHost = "localhost";
    public const int DefaultPort = 80;


    public static IServer Connect(Pipe pipes)
    { return Connect(new DefaultContainer(), DefaultHost, DefaultPort, pipes); }

    public static IServer Connect(string host, int port, Pipe pipes)
    { return Connect(new DefaultContainer(), host, port, pipes); }

    public static IServer Connect(this IContainer container, Pipe pipes)
    { return Connect(container, DefaultHost, DefaultPort, pipes); }

    public static IServer Connect(this IContainer container,
      string host, int port, Pipe pipes, params IService[] services)
    {
      var broker = container.BuildServicesBroker(services);
      var handler = container.BuildRequestHandler(pipes, broker);

      return container.BuildServer(host, port, handler);
    }
  }
}
