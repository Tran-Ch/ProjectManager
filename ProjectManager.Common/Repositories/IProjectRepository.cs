using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IProjectRepository<TProject>
    {
        IEnumerable<TProject> GetProjectsByEmployeeId(Guid employeeId);
        IEnumerable<TProject> GetProjectsByManagerId(Guid managerId);
        TProject GetProjectById(Guid Id);
        public void AddProject(TProject project);
        public void UpdateProject(TProject project);
        public void DeleteProject(Guid Id);
        public void AddMember(Guid projectId, Guid employeeId);
    }
}
