using ProjectManager.Common.Repositories;
using ProjectManager.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BLL.Mappers;

namespace ProjectManager.BLL.Services
{
    public class ProjectService : IProjectRepository<Project>
    {
        private readonly IProjectRepository<ProjectManager.DAL.Entities.Project> _dalService;

        public ProjectService(IProjectRepository<ProjectManager.DAL.Entities.Project> dalService)
        {
            _dalService = dalService;
        }

        public void AddMember(Guid projectId, Guid employeeId, DateTime startDate)
        {
            var project = GetProjectById(projectId);
            if (project != null && startDate < project.CreationDate)
            {
                throw new ArgumentException("Start date cannot be earlier than project creation date.");
            }
            _dalService.AddMember(projectId, employeeId, startDate);
        }

        public Guid AddProject(Project project)
        {
            return _dalService.AddProject(project.ToDAL());
        }

        public Project GetProjectById(Guid projectId)
        {
            var entity = _dalService.GetProjectById(projectId);
            return entity?.ToBLL();
        }

        public IEnumerable<Project> GetProjectsByEmployeeId(Guid employeeId)
        {
            return _dalService.GetProjectsByEmployeeId(employeeId).Select(p => p.ToBLL());
        }

        public IEnumerable<Project> GetProjectsByManagerId(Guid projectManagerId)
        {
            return _dalService.GetProjectsByManagerId(projectManagerId).Select(p => p.ToBLL());
        }

        public void RemoveMember(Guid projectId, Guid employeeId, DateTime endDate)
        {
            _dalService.RemoveMember(projectId, employeeId, endDate);
        }

        public void UpdateProject(Guid projectId, string description)
        {
            _dalService.UpdateProject(projectId, description);
        }
    }
}
