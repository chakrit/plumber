
using System;

using Plumber.Services;

namespace Plumber
{
  public interface IContext : IObjectsStore
  {
    IRequest Request { get; }
    IResponse Response { get; }

    IObjectsStore Store { get; }
    IServicesBroker Services { get; }
  }
}
