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

        public void AddPost(Post post)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(post.Subject), post.Subject);
                command.Parameters.AddWithValue(nameof(post.Content), post.Content);
                command.Parameters.AddWithValue(nameof(post.SendDate), post.SendDate);
                command.Parameters.AddWithValue(nameof(post.EmployeeId), post.EmployeeId);
                command.Parameters.AddWithValue(nameof(post.ProjectId), post.ProjectId);
                
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value) post.PostId = (Guid)result;
            }
        }

        public IEnumerable<Post> GetPostsByEmployeeId(Guid employeeId)
        {
            List<Post> posts = new List<Post>();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(employeeId), employeeId);
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        posts.Add(reader.ToPost());
                    }
                }
            }
            return posts;
        }

        public IEnumerable<Post> GetPostsByProjectManage(Guid projectId)
        {
            List<Post> posts = new List<Post>();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_ProjectManage";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(projectId), projectId);
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        posts.Add(reader.ToPost());
                    }
                }
            }
            return posts;
        }

        public void UpdatePost(Post post)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Update";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(nameof(post.PostId), post.PostId);
                command.Parameters.AddWithValue(nameof(post.Subject), post.Subject);
                command.Parameters.AddWithValue(nameof(post.Content), post.Content);
                command.Parameters.AddWithValue(nameof(post.SendDate), post.SendDate);
                command.Parameters.AddWithValue(nameof(post.EmployeeId), post.EmployeeId);
                command.Parameters.AddWithValue(nameof(post.ProjectId), post.ProjectId);
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
