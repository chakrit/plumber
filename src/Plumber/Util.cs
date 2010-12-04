
using System;

namespace Plumber
{
  internal static class Util
  {
    public static string F(this string format, object arg0)
    { return string.Format(format, arg0); }

    public static string F(this string format, object arg0, object arg1)
    { return string.Format(format, arg0, arg1); }

    public static string F(this string format, object arg0, object arg1, object arg2)
    { return string.Format(format, arg0, arg1, arg2); }

    public static string F(this string format, params object[] args)
    { return string.Format(format, args); }


    public static bool FastEquals(this string s, string another)
    { return string.Equals(s, another, StringComparison.OrdinalIgnoreCase); }
  }
}
