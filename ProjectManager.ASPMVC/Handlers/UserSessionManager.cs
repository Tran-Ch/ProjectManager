using System.Text.Json;

namespace ProjectManager.ASPMVC.Handlers
{
    public class UserSessionManager
    {
        private readonly ISession _session;

        public UserSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public Guid? EmployeeId
        {
            get
            {
                return JsonSerializer.Deserialize<Guid?>(_session.GetString(nameof(EmployeeId)) ?? "null");
            }
            set
            {
                if (value is null) _session.Remove(nameof(EmployeeId));
                else _session.SetString(nameof(EmployeeId), JsonSerializer.Serialize(value));
            }
        }

        public string? FullName
        {
            get => _session.GetString(nameof(FullName));
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _session.Remove(nameof(FullName));
                else _session.SetString(nameof(FullName), value);
            }
        }

        public bool IsProjectManager
        {
            get
            {
                return JsonSerializer.Deserialize<bool>(_session.GetString(nameof(IsProjectManager)) ?? "false");
            }
            set
            {
                _session.SetString(nameof(IsProjectManager), JsonSerializer.Serialize(value));
            }
        }

        public bool IsAuthenticated => EmployeeId is not null;

        public void Clear()
        {
            _session.Clear();
        }
    }
}