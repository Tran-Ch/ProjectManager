using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class ProjectManagerFilter : IAuthorizationFilter
    {
        private readonly UserSessionManager _userSession;

        public ProjectManagerFilter(UserSessionManager userSession)
        {
            _userSession = userSession;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_userSession.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (!_userSession.IsProjectManager)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}