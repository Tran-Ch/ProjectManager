using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IUserRepository<TUser>
    {
        Guid Add(TUser user);
        Guid? CheckPassword(string email, string password);
        TUser GetFromEmployee(Guid employeeId);
    }
}
