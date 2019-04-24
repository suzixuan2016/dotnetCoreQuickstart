using Castle.DynamicProxy;
using System;

namespace Bussiness
{
    /// <summary>
    /// 调用拦截器
    /// </summary>
    public class CallLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("开始调用方法");

            invocation.Proceed();

            Console.WriteLine("结束调用方法");
        }
    }
}
