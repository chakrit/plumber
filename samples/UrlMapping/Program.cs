﻿
using Plumber;
using Plumber.Framework;

namespace UrlMapping
{
  public class Program
  {
    internal static void Main(string[] args) { new Program().Run(); }


    public void Run()
    {
      var map = UrlMappings
        .New("/", Static.String("Hello, you just hit the front page!"))
        .Add("/test", Static.String("Hello, you just hit the test page!"))
        .Map();

      Pipes.Connect(map).Start();
    }
  }
}
