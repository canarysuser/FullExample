using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ProjectsMVCApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleManagerAttribute : System.Attribute, IActionFilter
    {
        string _requiredRole;
        public RoleManagerAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("RoleName");
            if (role != _requiredRole)
            {
                context.Result = new ViewResult() { ViewName = "Unauthorized" };
            }
        }
    }
}
