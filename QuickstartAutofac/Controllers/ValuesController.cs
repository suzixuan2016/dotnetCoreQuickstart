using Bussiness;
using Microsoft.AspNetCore.Mvc;
using QuickstartAutofac.Filter;

namespace QuickstartAutofac.Controllers
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [Route("api/value")]
    [ApiController]
    [TypeFilter(typeof(UserAuthrizeAttribute))]//需要认证
    public class ValuesController : ControllerBase
    {
        private readonly ITest _test;
        public ValuesController(ITest test)
        {
            _test = test;
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="name">输入参数</param>
        /// <param name="id"></param>
        /// <param name="cardId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet("test")]
        public string Test(string name,int id,double cardId,string pwd)
        {
            return _test.Add();
        }
    }
}
