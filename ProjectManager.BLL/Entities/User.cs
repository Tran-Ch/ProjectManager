using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Guid EmployeeId { get; private set; }
        public User(Guid userId, string email, string password, Guid employeeId)
        {
            UserId = userId;
            Email = email;
            Password = password;
            EmployeeId = employeeId;
        }
    }
}
