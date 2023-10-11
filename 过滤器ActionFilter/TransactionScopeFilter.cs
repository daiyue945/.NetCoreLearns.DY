using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace 过滤器ActionFilter
{
    //自动启用事务的过滤器
    public class TransactionScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //context.ActionDescriptor中是当前被执行的Action方法的描述信息
            //context.ActionArguments中是当前被执行的Action方法的参数信息
            ControllerActionDescriptor actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            bool isTX = false;//是否进行事务控制，默认不进行
            if (actionDescriptor != null)
            {
                //actionDescriptor.MethodInfo//当前的Action方法
                //是否标注【不进行事务控制】的属性
                bool hasNotTransactionAtrribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(NotTransataionAttribute),false).Any();
                //如果没有标注，则进行事务控制
                isTX = !hasNotTransactionAtrribute;
                if (isTX)
                {
                    using (TransactionScope tx=new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var r=await next();
                        //没有异常则全部完成
                        if (r.Exception == null)
                        {
                            tx.Complete();
                        }
                    }
                }
                else
                {
                    await next(); 
                }
            }
        }
    }
}
