using Autofac.Extras.DynamicProxy;

namespace Bussiness
{
    [Intercept(typeof(CallLogger))]
    public class Test: ITest
    {
        public string Add()
        {
            return "测试方法";
        }
    }
}
