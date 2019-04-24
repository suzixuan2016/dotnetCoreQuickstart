using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace QuickstartAutofac.Filter
{
    /// <summary>
    /// controller全局日志记录filter
    /// </summary>
    public class LogFilter:IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //
            Console.WriteLine($"调用的控制器：{context.Controller}");
            Console.WriteLine($"调用控制器的参数：{string.Join(",",context.ActionArguments.Keys.ToList())}");
            Console.WriteLine($"调用控制器的参数value：{string.Join(",", context.ActionArguments.Values.ToList())}");
            //
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
