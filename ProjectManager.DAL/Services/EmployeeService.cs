using ProjectManager.DAL.Entities;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using ProjectManager.DAL.Mappers;

namespace ProjectManager.DAL.Services
{
    public class EmployeeService : IEmployeeRepository<Employee>
    {
        private readonly SqlConnection _connection;

        public EmployeeService(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employee.EmployeeId), employee.EmployeeId);
                command.Parameters.AddWithValue(nameof(employee.FirstName), employee.FirstName);
                command.Parameters.AddWithValue(nameof(employee.LastName), employee.LastName);
                command.Parameters.AddWithValue(nameof(employee.Hiredate), employee.Hiredate);
                command.Parameters.AddWithValue(nameof(employee.Email), (object)employee.Email ?? DBNull.Value);
                command.Parameters.AddWithValue(nameof(employee.IsProjectManager), employee.IsProjectManager);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public IEnumerable<Employee> GetAvailableEmployees()
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_GetFree";
                command.CommandType = CommandType.StoredProcedure;
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
                _connection.Close();
            }

        }

        public Employee GetByEmail(string email)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(email), email);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToEmployee();
                    }
                    throw new ArgumentOutOfRangeException(nameof(email), $"No employee found with email: {email}");
                }
            }
        }

        public Employee GetEmployeeById(Guid EmployeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(EmployeeId), EmployeeId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToEmployee();
                    }
                    throw new ArgumentOutOfRangeException(nameof(EmployeeId), $"No employee found with Id: {EmployeeId}");
                }
                _connection.Close();
            }
        }

        public IEnumerable<Employee> GetProjectMember(Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromProjectId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
                _connection.Close();
            }
        }

        public void SetProjectManager(Guid id, bool isProjectManager)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Set_IsProjectManager";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(Employee.EmployeeId), id);
                command.Parameters.AddWithValue(nameof(Employee.IsProjectManager), isProjectManager);

                if (_connection.State != ConnectionState.Open) _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
