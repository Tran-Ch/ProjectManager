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
        IEnumerable<TProject> GetProjectsByManagerId(Guid projectManagerId);
        TProject GetProjectById(Guid projectId);
        Guid AddProject(TProject project);
        void UpdateProject(Guid projectId, string description);
        void AddMember(Guid projectId, Guid employeeId, DateTime startDate);
        void RemoveMember(Guid projectId, Guid employeeId, DateTime endDate);
    }
}
