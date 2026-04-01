using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Post;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    [TypeFilter<RequiredAuthenticationFilter>]
    public class PostController : Controller
    {
        private readonly IPostRepository<ProjectManager.BLL.Entities.Post> _postService;
        private readonly IProjectRepository<ProjectManager.BLL.Entities.Project> _projectService;
        private readonly IEmployeeRepository<ProjectManager.BLL.Entities.Employee> _employeeService;
        private readonly UserSessionManager _userSession;

        public PostController(
            IPostRepository<ProjectManager.BLL.Entities.Post> postService,
            IProjectRepository<ProjectManager.BLL.Entities.Project> projectService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            UserSessionManager userSession)
        {
            _postService = postService;
            _projectService = projectService;
            _employeeService = employeeService;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            LoadPostCount();

            Guid employeeId = _userSession.EmployeeId!.Value;

            IEnumerable<ProjectManager.BLL.Entities.Project> projects =
                _userSession.IsProjectManager
                ? _projectService.GetProjectsByManagerId(employeeId)
                : _projectService.GetProjectsByEmployeeId(employeeId);

            List<ListItemViewModel> model = new List<ListItemViewModel>();

            foreach (ProjectManager.BLL.Entities.Project project in projects)
            {
                IEnumerable<ProjectManager.BLL.Entities.Post> posts =
                    _userSession.IsProjectManager
                    ? _postService.GetPostsByProjectManager(project.ProjectId)
                    : _postService.GetPostsByEmployee(project.ProjectId, employeeId);

                model.AddRange(posts.Select(p => new ListItemViewModel
                {
                    PostId = p.PostId,
                    Subject = p.Subject,
                    Content = p.Content,
                    SendDate = p.SendDate,
                    ProjectId = p.ProjectId,
                    EmployeeId = p.EmployeeId
                }));
            }

            return View(model.OrderByDescending(p => p.SendDate));
        }

        [HttpGet]
        public IActionResult Create(Guid projectId)
        {
            LoadPostCount();

            Guid employeeId = _userSession.EmployeeId!.Value;
            ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(projectId);

            if (project is null)
            {
                return NotFound();
            }

            bool canManage = _userSession.IsProjectManager && project.ProjectManagerId == employeeId;
            bool canPost = canManage || _employeeService.CheckWorkOnProject(employeeId, projectId);

            if (!canPost)
            {
                return RedirectToAction("Index", "Project");
            }

            CreateForm model = new CreateForm
            {
                ProjectId = projectId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");

                Guid employeeId = _userSession.EmployeeId!.Value;
                ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(form.ProjectId);

                if (project is null)
                {
                    return NotFound();
                }

                bool canManage = _userSession.IsProjectManager && project.ProjectManagerId == employeeId;
                bool canPost = canManage || _employeeService.CheckWorkOnProject(employeeId, form.ProjectId);

                if (!canPost)
                {
                    return RedirectToAction("Index", "Project");
                }

                ProjectManager.BLL.Entities.Post post = new ProjectManager.BLL.Entities.Post(
                    Guid.Empty,
                    form.Subject,
                    form.Content,
                    DateTime.Now,
                    employeeId,
                    form.ProjectId
                );

                _postService.AddPost(post);

                return RedirectToAction("Details", "Project", new { id = form.ProjectId });
            }
            catch
            {
                LoadPostCount();
                return View(form);
            }
        }

        private void LoadPostCount()
        {
            Guid employeeId = _userSession.EmployeeId!.Value;

            IEnumerable<ProjectManager.BLL.Entities.Project> projects =
                _userSession.IsProjectManager
                ? _projectService.GetProjectsByManagerId(employeeId)
                : _projectService.GetProjectsByEmployeeId(employeeId);

            int postCount = 0;

            foreach (var project in projects)
            {
                var posts = _userSession.IsProjectManager
                    ? _postService.GetPostsByProjectManager(project.ProjectId)
                    : _postService.GetPostsByEmployee(project.ProjectId, employeeId);

                postCount += posts.Count();
            }

            ViewBag.PostCount = postCount;
        }
    }
}