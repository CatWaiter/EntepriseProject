using EnterpriseProject.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json.Serialization;

namespace EnterpriseProject.Models
{
    public class UserActivityFilter : IActionFilter
    {
        private readonly ApplicationDbContext context;

        public UserActivityFilter(ApplicationDbContext context)
        {

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var data = "";

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            var url = $"{controllerName}/{actionName}";

            if(!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var userData = context.ActionArguments.FirstOrDefault();
                var stringUserData = JsonConverter.SerializeObject(userData);

                data = stringUserData;
            }

            var userName = context.HttpContext.User.Identity.Name;

            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public void StoreUserActivity(string data, string url, string userName, string ipAddress)
        {

        }
    }
}
