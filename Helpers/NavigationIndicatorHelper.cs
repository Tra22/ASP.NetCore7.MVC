using Microsoft.AspNetCore.Mvc;

namespace MVC.Helpers{
    public static class NavigationIndicatorHelper
    {
        public static string? MakeActiveClass(this IUrlHelper urlHelper, string controller,string action)
        {
            try
            {
                string result = "active";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
                if (string.IsNullOrEmpty(controllerName)) return null;
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName != null && methodName.Contains(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}