using System.Web.Mvc;

namespace WEBIT_APP.Areas.Link
{
    public class LinkAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Link";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Link_default",
                "Link/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}