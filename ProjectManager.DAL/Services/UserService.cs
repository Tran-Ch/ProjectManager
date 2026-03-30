using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using ProjectManager.DAL.Mappers;

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

            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Salt", user.Salt);
            command.Parameters.AddWithValue("@EmployeeId", user.EmployeeId);

            if (_connection.State != ConnectionState.Open) _connection.Open();

            var result = command.ExecuteScalar();
            return (Guid)result;
        }
    }

    public Guid? CheckPassword(string email, string password)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SP_User_CheckPassword";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);

            if (_connection.State != ConnectionState.Open) _connection.Open();

            var result = command.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? null : (Guid?)result;
        }
    }

    public User GetFromEmployee(Guid employeeId)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SP_User_Get_FromEmployeeId";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@employeeId", employeeId);

            if (_connection.State != ConnectionState.Open) _connection.Open();

            // Sử dụng CloseConnection để tự đóng kết nối sau khi đọc xong
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (reader.Read())
                {
                    return reader.ToUser();
                }
                return null;
            }
        }
    }

    public bool EmailExists(string email)
    {
        return false;
    }
}