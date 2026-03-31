using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Hiredate { get; set; }
        public bool IsProjectManager { get; set; }
        public string Email { get; set; }
    }
}
