using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Project;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    [TypeFilter<RequiredAuthenticationFilter>]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository<ProjectManager.BLL.Entities.Project> _projectService;
        private readonly IEmployeeRepository<ProjectManager.BLL.Entities.Employee> _employeeService;
        private readonly IPostRepository<ProjectManager.BLL.Entities.Post> _postService;
        private readonly UserSessionManager _userSession;

        public ProjectController(
            IProjectRepository<ProjectManager.BLL.Entities.Project> projectService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            IPostRepository<ProjectManager.BLL.Entities.Post> postService,
            UserSessionManager userSession)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _postService = postService;
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

            IEnumerable<ListItemViewModel> model = projects.Select(p =>
            {
                var members = _employeeService.GetProjectMembers(p.ProjectId).ToList();

                ProjectManager.BLL.Entities.Employee manager = _employeeService.GetEmployeeById(p.ProjectManagerId);
                if (manager is not null && !members.Any(m => m.EmployeeId == manager.EmployeeId))
                {
                    members.Add(manager);
                }

                return new ListItemViewModel
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Description = p.Description,
                    CreationDate = p.CreationDate,
                    MemberCount = members.Count
                };
            });

            return View(model);
        }

        public IActionResult Details(Guid id)
        {
            LoadPostCount();

            Guid employeeId = _userSession.EmployeeId!.Value;

            ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(id);

            if (project is null)
            {
                return NotFound();
            }

            bool canManage = _userSession.IsProjectManager && project.ProjectManagerId == employeeId;
            bool canAccess = canManage || _employeeService.CheckWorkOnProject(employeeId, id);

            if (!canAccess)
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<ProjectManager.BLL.Entities.Post> posts = canManage
                ? _postService.GetPostsByProjectManager(id)
                : _postService.GetPostsByEmployee(id, employeeId);

            List<ProjectManager.BLL.Entities.Employee> members = _employeeService.GetProjectMembers(id).ToList();

            ProjectManager.BLL.Entities.Employee manager = _employeeService.GetEmployeeById(project.ProjectManagerId);

            if (manager is not null && !members.Any(m => m.EmployeeId == manager.EmployeeId))
            {
                members.Add(manager);
            }

            DetailsViewModel model = new DetailsViewModel
            {
                ProjectId = project.ProjectId,
                ProjectManagerId = project.ProjectManagerId,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate,
                CanManageProject = canManage,
                Members = members,
                Posts = posts
            };

            return View(model);
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpGet]
        public IActionResult Create()
        {
            LoadPostCount();
            return View();
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");

                ProjectManager.BLL.Entities.Project project = new ProjectManager.BLL.Entities.Project(
                    Guid.Empty,
                    form.Name,
                    form.Description,
                    DateTime.Now,
                    _userSession.EmployeeId!.Value
                );

                Guid projectId = _projectService.AddProject(project);

                return RedirectToAction(nameof(Details), new { id = projectId });
            }
            catch
            {
                LoadPostCount();
                return View(form);
            }
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpGet]
        public IActionResult EditDescription(Guid id)
        {
            LoadPostCount();

            ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(id);

            if (project is null)
            {
                return NotFound();
            }

            if (project.ProjectManagerId != _userSession.EmployeeId!.Value)
            {
                return RedirectToAction(nameof(Index));
            }

            EditDescriptionForm model = new EditDescriptionForm
            {
                ProjectId = project.ProjectId,
                Description = project.Description
            };

            return View(model);
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDescription(EditDescriptionForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");

                ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(form.ProjectId);

                if (project is null)
                {
                    return NotFound();
                }

                if (project.ProjectManagerId != _userSession.EmployeeId!.Value)
                {
                    return RedirectToAction(nameof(Index));
                }

                _projectService.UpdateProject(form.ProjectId, form.Description);

                return RedirectToAction(nameof(Details), new { id = form.ProjectId });
            }
            catch
            {
                LoadPostCount();
                return View(form);
            }
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpGet]
        public IActionResult AddMember(Guid id)
        {
            LoadPostCount();

            ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(id);

            if (project is null)
            {
                return NotFound();
            }

            if (project.ProjectManagerId != _userSession.EmployeeId!.Value)
            {
                return RedirectToAction(nameof(Index));
            }

            AddMemberForm model = new AddMemberForm
            {
                ProjectId = id
            };

            ViewBag.Employees = _employeeService.GetAvailableEmployees();
            return View(model);
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(AddMemberForm form)
        {
            try
            {
                ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(form.ProjectId);

                if (project is null)
                {
                    return NotFound();
                }

                if (project.ProjectManagerId != _userSession.EmployeeId!.Value)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Employees = _employeeService.GetAvailableEmployees();
                    throw new InvalidOperationException("Le formulaire n'est pas valide.");
                }

                _projectService.AddMember(form.ProjectId, form.EmployeeId, DateTime.Now);

                return RedirectToAction(nameof(Details), new { id = form.ProjectId });
            }
            catch
            {
                LoadPostCount();
                ViewBag.Employees = _employeeService.GetAvailableEmployees();
                return View(form);
            }
        }

        [TypeFilter<ProjectManagerFilter>]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveMember(Guid projectId, Guid employeeId)
        {
            ProjectManager.BLL.Entities.Project project = _projectService.GetProjectById(projectId);

            if (project is null)
            {
                return NotFound();
            }

            if (project.ProjectManagerId != _userSession.EmployeeId!.Value)
            {
                return RedirectToAction(nameof(Index));
            }

            _projectService.RemoveMember(projectId, employeeId, DateTime.Now);

            return RedirectToAction(nameof(Details), new { id = projectId });
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