using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Mappers
{
    internal static class Mapper
    {
        #region Employee

        public static ProjectManager.BLL.Entities.Employee ToBLL(this ProjectManager.DAL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            ProjectManager.BLL.Entities.Employee employee = new ProjectManager.BLL.Entities.Employee(
                entity.EmployeeId,
                entity.Firstname,
                entity.Lastname,
                entity.Hiredate,
                entity.IsProjectManager,
                entity.Email
            );
            return employee;
        }

        public static ProjectManager.DAL.Entities.Employee ToDAL(this ProjectManager.BLL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.DAL.Entities.Employee()
            {
                EmployeeId = entity.EmployeeId,
                Firstname = entity.FirstName,
                Lastname = entity.LastName,
                Hiredate = entity.Hiredate,
                IsProjectManager = entity.IsProjectManager,
                Email = entity.Email
            };
        }

        #endregion

        #region Post

        public static ProjectManager.BLL.Entities.Post ToBLL(this ProjectManager.DAL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            ProjectManager.BLL.Entities.Post post = new ProjectManager.BLL.Entities.Post(
                entity.PostId,
                entity.Subject,
                entity.Content,
                entity.SendDate,
                entity.EmployeeId,
                entity.ProjectId
            );
            return post;
        }

        public static ProjectManager.DAL.Entities.Post ToDAL(this ProjectManager.BLL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.DAL.Entities.Post()
            {
                PostId = entity.PostId,
                Subject = entity.Subject,
                Content = entity.Content,
                SendDate = entity.SendDate,
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId
            };
        }

        #endregion

        #region Project

        public static ProjectManager.BLL.Entities.Project ToBLL(this ProjectManager.DAL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ProjectManager.BLL.Entities.Project project = new ProjectManager.BLL.Entities.Project(
                entity.ProjectId,
                entity.Name,
                entity.Description,
                entity.CreationDate,
                entity.ProjectManagerId
            );
            return project;
        }

        public static ProjectManager.DAL.Entities.Project ToDAL(this ProjectManager.BLL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.DAL.Entities.Project()
            {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                CreationDate = entity.CreationDate,
                ProjectManagerId = entity.ProjectManagerId
            };
        }

        #endregion

        #region TakePart

        public static ProjectManager.BLL.Entities.TakePart ToBLL(this ProjectManager.DAL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.BLL.Entities.TakePart(
                entity.ProjectId,
                entity.EmployeeId,
                entity.StartDate,
                entity.EndDate
            );
        }

        public static ProjectManager.DAL.Entities.TakePart ToDAL(this ProjectManager.BLL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.DAL.Entities.TakePart()
            {
                ProjectId = entity.ProjectId,
                EmployeeId = entity.EmployeeId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }

        #endregion

        #region User

        public static ProjectManager.BLL.Entities.User ToBLL(this ProjectManager.DAL.Entities.User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ProjectManager.BLL.Entities.User user = new ProjectManager.BLL.Entities.User(
                entity.UserId,
                entity.Email,
                entity.Password,
                entity.EmployeeId
            );
            return user;
        }

        public static ProjectManager.DAL.Entities.User ToDAL(this ProjectManager.BLL.Entities.User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new ProjectManager.DAL.Entities.User()
            {
                UserId = entity.UserId,
                Email = entity.Email,
                Password = entity.Password,
                EmployeeId = entity.EmployeeId,
                Salt = Guid.NewGuid()
            };
        }

        #endregion
    }
}
