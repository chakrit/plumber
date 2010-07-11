
namespace Plumber
{
  public static class Pipes
  {
    public static IContainer GetContainer()
    {
      return new DefaultContainer();
    }

    public static IServer Connect(string host, int port, Pipe pipes)
    {
      return Connect(GetContainer(), host, port, pipes);
    }

    public static IServer Connect(this IContainer container,
      string host, int port, Pipe pipes)
    {
      return container.BuildServer(host, port,
        container.BuildRequestHandler(pipes));
    }
  }
}
