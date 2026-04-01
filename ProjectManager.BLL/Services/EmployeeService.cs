using ProjectManager.BLL.Entities;
using ProjectManager.BLL.Mappers;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class EmployeeService : IEmployeeRepository<Employee>
    {
        private readonly IEmployeeRepository<ProjectManager.DAL.Entities.Employee> _dalService;

        public EmployeeService(IEmployeeRepository<DAL.Entities.Employee> dalService)
        {
            _dalService = dalService;
        }

        public Guid AddEmployee(Employee employee)
        {
            return _dalService.AddEmployee(employee.ToDAL());
        }

        public bool CheckIsProjectManager(Guid employeeId)
        {
            return _dalService.CheckIsProjectManager(employeeId);
        }

        public bool CheckWorkOnProject(Guid employeeId, Guid projectId)
        {
            return _dalService.CheckWorkOnProject(employeeId, projectId);
        }

        public IEnumerable<Employee> GetAvailableEmployees()
        {
            return _dalService.GetAvailableEmployees().Select(e => e.ToBLL());
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            var entity = _dalService.GetEmployeeById(employeeId);
            return entity != null ? entity.ToBLL() : null;
        }

        public Employee GetEmployeeByUserId(Guid userId)
        {
            var entity = _dalService.GetEmployeeByUserId(userId);
            return entity != null ? entity.ToBLL() : null;
        }

        public IEnumerable<Employee> GetProjectMembers(Guid projectId)
        {
            return _dalService.GetProjectMembers(projectId).Select(e => e.ToBLL());
        }

        public void SetProjectManager(Guid employeeId)
        {
            _dalService.SetProjectManager(employeeId);
        }
    }
}