using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DAL.Entities;
using System.Data;
using ProjectManager.DAL.Mappers;

namespace ProjectManager.DAL.Services
{
    public class PostService : IPostRepository<Post>
    {
        private readonly SqlConnection _connection;

        public PostService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Guid AddPost(Post post)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(post.EmployeeId), post.EmployeeId);
                command.Parameters.AddWithValue(nameof(post.ProjectId), post.ProjectId);
                command.Parameters.AddWithValue(nameof(post.Subject), post.Subject);
                command.Parameters.AddWithValue(nameof(post.Content), post.Content);

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

        public IEnumerable<Post> GetPostsByProjectManager(Guid projectId)
        {
            List<Post> posts = new List<Post>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_ProjectManager";
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
                            posts.Add(reader.ToPost());
                        }
                    }

                    return posts;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public IEnumerable<Post> GetPostsByEmployee(Guid projectId, Guid employeeId)
        {
            List<Post> posts = new List<Post>();

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(projectId), projectId);
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);

                try
                {
                    if (_connection.State != ConnectionState.Open)
                        _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            posts.Add(reader.ToPost());
                        }
                    }

                    return posts;
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }
        }

        public void UpdatePost(Guid postId, Guid employeeId, string content)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Update";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(nameof(postId), postId);
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                command.Parameters.AddWithValue(nameof(content), content);

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
