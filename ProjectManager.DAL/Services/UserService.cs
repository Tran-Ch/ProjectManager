using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using ProjectManager.DAL.Mappers;

namespace ProjectManager.DAL.Services
{
    public class UserService : IUserRepository<User>
    {
        private readonly SqlConnection _connection;
        public UserService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid Add(User user)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_User_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(user.EmployeeId), user.EmployeeId);
                command.Parameters.AddWithValue(nameof(user.Email), user.Email);
                command.Parameters.AddWithValue(nameof(user.Password), user.Password);

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

        public Guid? CheckPassword(string email, string password)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_User_CheckPassword";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(email), email);
                command.Parameters.AddWithValue(nameof(password), password);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    object result = command.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        return null;
                    }

                    return (Guid)result;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public User GetFromEmployee(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_User_Get_FromEmployeeId";
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
                            return reader.ToUser();
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
    }
}