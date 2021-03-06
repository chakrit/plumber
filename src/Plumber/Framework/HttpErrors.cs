﻿// <auto-generated />

namespace Plumber.Framework
{
  public static class HttpErrors
  {
    public static Pipe BadRequest()
    {
      return Custom(400, "BadRequest");
    }

    public static Pipe Unauthorized()
    {
      return Custom(401, "Unauthorized");
    }

    public static Pipe Forbidden()
    {
      return Custom(403, "Forbidden");
    }

    public static Pipe NotFound()
    {
      return Custom(404, "NotFound");
    }

    public static Pipe MethodNotAllowed()
    {
      return Custom(405, "MethodNotAllowed");
    }

    public static Pipe ServerError()
    {
      return Custom(500, "ServerError");
    }

    public static Pipe ServiceUnavailable()
    {
      return Custom(503, "ServiceUnavailable");
    }


    public static Pipe Custom(int statusCode, string statusMsg)
    {
      return (ctx, next) =>
      {
        ctx.Response.StatusCode = statusCode;
        ctx.Response.StatusMessage = statusMsg;

        next(ctx);
      };
    }
  }
}
