using ICT_LAB_WEB_API.Helper;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeMVC : AuthorizeAttribute
    {
        public string UserRole { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["User_Role"] == null)
            {
                return false;
            }
            
            string currentUserRole = httpContext.Session["User_Role"].ToString();
            if (UserRole.Contains(currentUserRole.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}