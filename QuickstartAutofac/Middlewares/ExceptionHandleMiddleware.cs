using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QuickstartAutofac.Middlewares
{
    /// <summary>
    /// 自定义异常处理中间件
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            ExceptionOutput message = null;
            try
            {
                await _next(context);
            }
            catch (ArgumentException e)
            {
                context.Response.StatusCode = 400;
                message = new ExceptionOutput
                {
                    Code = 40000,
                    Message = e.Message,
                    Detail = e.ToString()
                };
            }
            catch (AuthenticationException e)
            {
                context.Response.StatusCode = 401;
                message = new ExceptionOutput
                {
                    Code = 40100,
                    Message = e.Message,
                    Detail = e.ToString()
                };
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                message = new ExceptionOutput
                {
                    Code = 50000,
                    Message = e.Message,
                    Detail = e.ToString()
                };
            }
            finally
            {
                if (context.Response.StatusCode != 200 || message != null)
                {
                    if (message == null)
                    {
                        message = new ExceptionOutput
                        {
                            Code = context.Response.StatusCode,
                            Message = "未知异常",
                            Detail = "未知异常"
                        };
                    }

                    var messageStr = JsonConvert.SerializeObject(message);

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(messageStr);
                }
            }
        }

        public class ExceptionOutput
        {
            public int Code { get; set; }

            public string Message { get; set; }

            public string Detail { get; set; }
        }
    }
}
