using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IUserRepository<TUser>
    {
        public Guid Add(TUser user);
        public Guid? CheckPassword(string email, string password);
        public TUser GetFromEmployee(Guid employeeId);
        bool EmailExists(string email);
    }
}
