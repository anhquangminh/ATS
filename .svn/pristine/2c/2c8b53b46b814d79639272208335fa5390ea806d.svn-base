using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATS_AlarmTrackingSystem.Models
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["UserName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "ATS" },
                                { "Action", "Login" }
                                });
            }
        }
    }
}