
using System;

namespace Plumber 
{
  public static partial class Pipes
  {
    public static Pipe Identity = ctx => ctx;

    public static Pipe Action(Action act)
    {
      return ctx => { act(); return ctx; };
    }

    public static Pipe Action<T1>(Action<T1> act,
      T1 arg1) 
    {
      return ctx => { act(arg1); return ctx; }; 
    }

    public static Pipe Action<T1, T2>(Action<T1, T2> act,
      T1 arg1, T2 arg2) 
    {
      return ctx => { act(arg1, arg2); return ctx; }; 
    }

    public static Pipe Action<T1, T2, T3>(Action<T1, T2, T3> act,
      T1 arg1, T2 arg2, T3 arg3) 
    {
      return ctx => { act(arg1, arg2, arg3); return ctx; }; 
    }

    public static Pipe Action<T1, T2, T3, T4>(Action<T1, T2, T3, T4> act,
      T1 arg1, T2 arg2, T3 arg3, T4 arg4) 
    {
      return ctx => { act(arg1, arg2, arg3, arg4); return ctx; }; 
    }

    public static Pipe Action<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> act,
      T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) 
    {
      return ctx => { act(arg1, arg2, arg3, arg4, arg5); return ctx; }; 
    }

    public static Pipe Action<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> act,
      T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) 
    {
      return ctx => { act(arg1, arg2, arg3, arg4, arg5, arg6); return ctx; }; 
    }

    public static Pipe Action<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> act,
      T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) 
    {
      return ctx => { act(arg1, arg2, arg3, arg4, arg5, arg6, arg7); return ctx; }; 
    }

  }
}

