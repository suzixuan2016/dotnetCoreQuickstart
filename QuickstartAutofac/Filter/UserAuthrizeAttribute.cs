using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuickstartAutofac.Filter
{
    /// <summary>
    /// 自定义用户认证
    /// </summary>
    public class UserAuthrizeAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var token = actionContext.HttpContext.Request.Headers["TokenKey"];
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("请传递tokenKey");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
