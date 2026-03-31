using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Auth;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository<ProjectManager.BLL.Entities.User> _userService;
        private readonly IEmployeeRepository<ProjectManager.BLL.Entities.Employee> _employeeService;
        private readonly UserSessionManager _sessionManager;

        public AuthController (
            IUserRepository<ProjectManager.BLL.Entities.User> userService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            UserSessionManager sessionManager)
        {
             _userService = userService;
            _employeeService = employeeService;
            _sessionManager = sessionManager;
        }

        [HttpGet]
        [TypeFilter<AnonymousFilter>]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter<AnonymousFilter>]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            Guid? employeeId = _userService.CheckPassword(form.Email, form.Password);

            if (employeeId is null) 
            {
                ModelState.AddModelError(nameof(form.Email), "Email ou mot de passe incorrect.");
                return View(form);
            }

            ProjectManager.BLL.Entities.Employee employee = _employeeService.GetEmployeeById(employeeId.Value);

            _sessionManager.SetUser(
                employee.EmployeeId, $"{employee.FirstName} {employee.LastName}",
                employee.IsProjectManager);

            return RedirectToAction("Index", "Home");
        }

        [RequiredAuthenticationFilter]
        public IActionResult Logout()
        {
            _sessionManager.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
