using System.Web.Mvc;

namespace LayuiPortal.Areas.EasyuiManager
{
    public class EasyuiManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EasyuiManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EasyuiManager_default",
                "EasyuiManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}