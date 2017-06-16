using System;
using System.Web.Mvc;

namespace ForumPlMvc.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class IdValidatorAttribute : FilterAttribute, IActionFilter
    { 
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.ActionParameters["id"];
            if ((id == null) || ((int)id < 1))
            {
                filterContext.Result = new RedirectResult("/Home/Error");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        { }
    }
}