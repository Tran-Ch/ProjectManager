using Microsoft.AspNetCore.Mvc;

namespace ProjectManager.ASPMVC.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
