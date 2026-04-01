using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class RequiredAuthenticationFilter : IAuthorizationFilter
    {
        private readonly UserSessionManager _userSession;

        public RequiredAuthenticationFilter(UserSessionManager userSession)
        {
            _userSession = userSession;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_userSession.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}