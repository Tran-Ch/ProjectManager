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

        public Guid AddEmployee(Employee employee)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employee.Firstname), employee.Firstname);
                command.Parameters.AddWithValue(nameof(employee.Lastname), employee.Lastname);
                command.Parameters.AddWithValue(nameof(employee.Hiredate), employee.Hiredate);

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

        public IEnumerable<Employee> GetAvailableEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_GetFree";
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(reader.ToEmployee());
                        }
                    }

                    return employees;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.ToEmployee();
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

        public Employee GetEmployeeByUserId(Guid userId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromUserId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(userId), userId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.ToEmployee();
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

        public IEnumerable<Employee> GetProjectMembers(Guid projectId)
        {
            List<Employee> employees = new List<Employee>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromProjectId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(projectId), projectId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(reader.ToEmployee());
                        }
                    }

                    return employees;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public void SetProjectManager(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Set_IsProjectManager";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

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

        public bool CheckIsProjectManager(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Check_IsProjectManager";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    object result = command.ExecuteScalar();
                    return result != null && result != DBNull.Value && (bool)result;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public bool CheckWorkOnProject(Guid employeeId, Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Check_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                command.Parameters.AddWithValue(nameof(projectId), projectId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    object result = command.ExecuteScalar();
                    return result != null && result != DBNull.Value;
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