using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Mappers
{
    internal static class Mapper
    {
        public static Employee ToEmployee(this IDataRecord record)
        {
            if (record is null) throw new ArgumentNullException(nameof(record));
            return new Employee()
            {
                EmployeeId = (Guid)record[nameof(Employee.EmployeeId)],
                Firstname = (string)record[nameof(Employee.Firstname)],
                Lastname = (string)record[nameof(Employee.Lastname)],
                Hiredate = (DateTime)record[nameof(Employee.Hiredate)],
                IsProjectManager = (bool)record[nameof(Employee.IsProjectManager)],
                Email = (string)record[nameof(Employee.Email)]
            };
        }

        public static Post ToPost(this IDataRecord record)
        {
            if (record is null) throw new ArgumentNullException(nameof(record));
            return new Post()
            {
                PostId = (Guid)record[nameof(Post.PostId)],
                Subject = (string)record[nameof(Post.Subject)],
                Content = (string)record[nameof(Post.Content)],
                SendDate = (DateTime)record[nameof(Post.SendDate)],
                EmployeeId = (Guid)record[nameof(Post.EmployeeId)],
                ProjectId = (Guid)record[nameof(Post.ProjectId)]
            };
        }

        public static Project ToProject(this IDataRecord record)
        {
            if (record is null) throw new ArgumentNullException(nameof(record));
            return new Project()
            {
                ProjectId = (Guid)record[nameof(Project.ProjectId)],
                Name = (string)record[nameof(Project.Name)],
                Description = (string)record[nameof(Project.Description)],
                CreationDate = (DateTime)record[nameof(Project.CreationDate)],
                ProjectManagerId = (Guid)record[nameof(Project.ProjectManagerId)]
            };
        }

        public static TakePart ToTakePart(this IDataRecord record)
        {
            if (record is null) throw new ArgumentNullException(nameof(record));
            return new TakePart()
            {
                EmployeeId = (Guid)record[nameof(TakePart.EmployeeId)],
                ProjectId = (Guid)record[nameof(TakePart.ProjectId)],
                StartDate = (DateTime)record[nameof(TakePart.StartDate)],
                EndDate = record[nameof(TakePart.EndDate)] is DBNull ? null : (DateTime?)record[nameof(TakePart.EndDate)]
            };
        }

        public static User ToUser(this IDataRecord record)
        {
            if (record is null) throw new ArgumentNullException(nameof(record));
            return new User()
            {
                UserId = (Guid)record[nameof(User.UserId)],
                Email = (string)record[nameof(User.Email)],
                Password = (string)record[nameof(User.Password)],
                Salt = Guid.Empty,
                EmployeeId = (Guid)record[nameof(User.EmployeeId)]
            };
        }
    }
}