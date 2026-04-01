using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class AnonymousFilter : IAuthorizationFilter
    {
        private readonly UserSessionManager _userSession;

        public AnonymousFilter(UserSessionManager userSession)
        {
            _userSession = userSession;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_userSession.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
