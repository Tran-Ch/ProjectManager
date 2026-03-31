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
        public ProjectService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid AddProject(Project project)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Insert";
                command.CommandType = CommandType.StoredProcedure;

                Guid projectManagerId = project.ProjectManagerId;

                command.Parameters.AddWithValue(nameof(projectManagerId), projectManagerId);
                command.Parameters.AddWithValue(nameof(project.Name), project.Name);
                command.Parameters.AddWithValue(nameof(project.Description), project.Description);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    return (Guid)command.ExecuteScalar();
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public IEnumerable<Project> GetProjectsByEmployeeId(Guid employeeId)
        {
            List<Project> projects = new List<Project>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(reader.ToProject());
                        }
                    }

                    return projects;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public IEnumerable<Project> GetProjectsByManagerId(Guid projectManagerId)
        {
            List<Project> projects = new List<Project>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromProjectManagerId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(projectManagerId), projectManagerId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(reader.ToProject());
                        }
                    }

                    return projects;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public Project GetProjectById(Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_ById";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(projectId), projectId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.ToProject();
                        }
                    }

                    return null;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public void UpdateProject(Guid projectId, string description)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Update";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue(nameof(description), description);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    command.ExecuteNonQuery();
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public void AddMember(Guid projectId, Guid employeeId, DateTime startDate)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_TakePart_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue(nameof(startDate), startDate);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    command.ExecuteNonQuery();
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public void RemoveMember(Guid projectId, Guid employeeId, DateTime endDate)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_TakePart_SetEnd";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue(nameof(endDate), endDate);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    command.ExecuteNonQuery();
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }
    }
}