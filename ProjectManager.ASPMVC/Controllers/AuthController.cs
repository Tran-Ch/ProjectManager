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
        private readonly UserSessionManager _userSession;

        public AuthController(
            IUserRepository<ProjectManager.BLL.Entities.User> userService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            UserSessionManager userSession)
        {
            _userService = userService;
            _employeeService = employeeService;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
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
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");

                Guid? employeeId = _userService.CheckPassword(form.Email, form.Password);

                if (employeeId is null)
                {
                    ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
                    return View(form);
                }

                ProjectManager.BLL.Entities.Employee employee = _employeeService.GetEmployeeById(employeeId.Value);

                if (employee is null)
                {
                    ModelState.AddModelError(string.Empty, "Employé introuvable.");
                    return View(form);
                }

                _userSession.EmployeeId = employee.EmployeeId;
                _userSession.FullName = $"{employee.FirstName} {employee.LastName}";
                _userSession.IsProjectManager = employee.IsProjectManager;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }

        [HttpGet]
        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Logout()
        {
            _userSession.Clear();
            return RedirectToAction(nameof(Login));
        }

        /*[HttpPost]
        [TypeFilter<RequiredAuthenticationFilter>]
        [ValidateAntiForgeryToken]
        public IActionResult Logout(IFormCollection collection)
        {
            try
            {
                _userSession.Clear();
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }*/
    }
}