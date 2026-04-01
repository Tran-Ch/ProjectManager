using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Models;
using System.Diagnostics;

namespace ProjectManager.ASPMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserSessionManager _userSession;

        public HomeController(
            ILogger<HomeController> logger,
            UserSessionManager userSession)
        {
            _logger = logger;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (_userSession.IsAuthenticated)
            {
                return RedirectToAction("Index", "Project");
            }

            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}