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
        TEmployee GetEmployeeById(Guid Id);
        TEmployee GetByEmail(string email);
        IEnumerable<TEmployee> GetProjectMember(Guid projectId);
        void AddEmployee(TEmployee employee);
        //void UpdateEmployee(TEmployee employee);
        //void DeleteEmployee(Guid Id);
        void SetProjectManager(Guid id, bool isProjectManager);

    }
}
