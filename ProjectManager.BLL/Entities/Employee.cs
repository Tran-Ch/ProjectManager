using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Hiredate { get; private set; }
        public bool IsProjectManager { get; private set; }
        public string Email { get; private set; }

        public Employee(Guid employeeId, string firstName, string lastName, DateTime hiredate, bool isProjectManager, string email)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Hiredate = hiredate;
            IsProjectManager = isProjectManager;
            Email = email;
        }
    }
}
