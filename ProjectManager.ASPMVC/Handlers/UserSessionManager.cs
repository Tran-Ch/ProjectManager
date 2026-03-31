namespace ProjectManager.ASPMVC.Handlers
{
    public class UserSessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetUser(Guid employeeId, string fullName, bool isProjectManager)
        { 
            _httpContextAccessor.HttpContext.Session.SetString("EmployeeId", employeeId.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("FullName", fullName);
            _httpContextAccessor.HttpContext.Session.SetString("IsProjectManager", isProjectManager.ToString());
        }

        public void Clear()
        { 
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("EmployeeId") is not null;
        }

        public Guid? GetEmployeeId()
        { 
            string value = _httpContextAccessor.HttpContext.Session.GetString("EmployeeId");
            if (Guid.TryParse(value, out Guid employeeId))
            {
                return employeeId;
            }
            return null;
        }

        public string GetFullName()
        { 
            return _httpContextAccessor.HttpContext.Session.GetString("FullName");
        }

        public bool IsProjectManager()
        {
            string value = _httpContextAccessor.HttpContext.Session.GetString("IsProjectManager");
            return bool.TryParse(value, out bool result) && result;
        }
    }
}
