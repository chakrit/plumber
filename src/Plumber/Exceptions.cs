
using System;
using System.Collections.Generic;

namespace Plumber
{
  internal static class Exceptions
  {
    public static KeyNotFoundException ObjKeyNotFound<T>()
    {
      var msg = "There is no object of type {0} available in the store."
        .F(typeof(T).FullName);

      return new KeyNotFoundException(msg);
    }

    public static ArgumentException CannotProvideObj<T>()
    {
      var msg = "Current context doesn't have an object of type {0} available" +
        " and there is no services that provides it."
        .F(typeof(T).FullName);

      return new ArgumentException(msg);
    }
  }
}
