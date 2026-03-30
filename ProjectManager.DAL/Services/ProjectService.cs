using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using ProjectManager.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectManager.DAL.Services
{
    public class ProjectService : IProjectRepository<Project>
    {
        private readonly SqlConnection _connection;
        public ProjectService(SqlConnection connection) => _connection = connection;

        public void AddMember(Guid projectId, Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_AddMember";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@projectId", projectId);
                command.Parameters.AddWithValue("@employeeId", employeeId);

                if (_connection.State != ConnectionState.Open) _connection.Open();
                command.ExecuteNonQuery(); 
            }
        }

        public void AddProject(Project project)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", project.Name);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@Creationdate", project.Creationdate);
                command.Parameters.AddWithValue("@ProjectManagerId", project.ProjectManagerId);

                if (_connection.State != ConnectionState.Open) _connection.Open();

                var result = command.ExecuteScalar();
                if (result != null) project.ProjectId = (Guid)result;
            }
        }

        public IEnumerable<Project> GetProjectsByEmployeeId(Guid employeeId)
        {
            List<Project> projects = new List<Project>();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employeeId", employeeId);

                if (_connection.State != ConnectionState.Open) _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(reader.ToProject());
                    }
                }
            }
            return projects;
        }

        public void DeleteProject(Guid Id) => throw new NotImplementedException();
        public void UpdateProject(Project project) => throw new NotImplementedException();
        public Project GetProjectById(Guid Id) => throw new NotImplementedException();
        public IEnumerable<Project> GetProjectsByManagerId(Guid managerId) => throw new NotImplementedException();
    }
}
