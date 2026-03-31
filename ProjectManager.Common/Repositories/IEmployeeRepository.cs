using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectManager.Common.Repositories
{
    public interface IEmployeeRepository<TEmployee>
    {
        IEnumerable<TEmployee> GetAvailableEmployees();
        TEmployee GetEmployeeById(Guid employeeId);
        TEmployee GetEmployeeByUserId(Guid userId);
        IEnumerable<TEmployee> GetProjectMembers(Guid projectId);
        Guid AddEmployee(TEmployee employee);
        void SetProjectManager(Guid employeeId);
        bool CheckIsProjectManager(Guid employeeId);
        bool CheckWorkOnProject(Guid employeeId, Guid projectId);

    }
}
