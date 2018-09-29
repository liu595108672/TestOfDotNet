using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TestForWebAPI
{

    public class MyFilters:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var controllerName = context.Controller.ToString();
            var tmp = typeof(Controllers.ValuesController);
            if (tmp.Name==controllerName)
            {
                return;
            }
            return;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            context.Result = new RedirectResult("/api/Options");
        }
    }

    public class MyFilter2 : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tmpHeader= context.HttpContext.Request.Headers;
        }
    }
}
