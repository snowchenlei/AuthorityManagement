using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cl.AuthorityManagement.Web.App_Start
{
    public class CallLogger: IInterceptor
    {
        /// <summary>
        /// 拦截方法 打印被拦截的方法执行前的名称、参数和方法执行后的 返回结果
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {

            string para = $"你正在调用方法 {invocation.Method.Name}参数是 {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}";

            //在被拦截的方法执行完毕后 继续执行
            invocation.Proceed();

            para = $"方法执行完毕，返回结果：{invocation.ReturnValue}";
        }
    }
}