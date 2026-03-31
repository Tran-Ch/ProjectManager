using ProjectManager.Common.Repositories;
using ProjectManager.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BLL.Mappers;

namespace ProjectManager.BLL.Services
{
    public class UserService : IUserRepository<User>
    {
        private readonly IUserRepository<ProjectManager.DAL.Entities.User> _dalService;

        public UserService(IUserRepository<ProjectManager.DAL.Entities.User> dalService)
        {
            _dalService = dalService;
        }

        public Guid Add(User user)
        {
            return _dalService.Add(user.ToDAL());
        }

        public Guid? CheckPassword(string email, string password)
        {
            return _dalService.CheckPassword(email, password);
        }

        public User GetFromEmployee(Guid employeeId)
        {
            var entity = _dalService.GetFromEmployee(employeeId);
            return entity != null ? entity.ToBLL() : null;
        }
    }
}
