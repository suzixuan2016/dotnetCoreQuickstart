using Autofac;
using Autofac.Extras.DynamicProxy;

namespace Bussiness
{
    public class BussinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //IInterceptor需要提前注入进来
            builder.Register(c => new CallLogger());


            builder.RegisterType<Test>().As<ITest>().EnableInterfaceInterceptors();
        }
    }
}
