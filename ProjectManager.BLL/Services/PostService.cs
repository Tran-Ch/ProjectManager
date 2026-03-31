using ProjectManager.Common.Repositories;
using ProjectManager.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BLL.Mappers;

namespace ProjectManager.BLL.Services
{
    public class PostService : IPostRepository<Post>
    {
        private readonly IPostRepository<ProjectManager.DAL.Entities.Post> _dalService;

        public PostService(IPostRepository<ProjectManager.DAL.Entities.Post> dalService)
        {
            _dalService = dalService;
        }

        public Guid AddPost(Post post)
        {
            return _dalService.AddPost(post.ToDAL());
        }

        public IEnumerable<Post> GetPostsByEmployee(Guid projectId, Guid employeeId)
        {
            var dalPosts = _dalService.GetPostsByEmployee(projectId, employeeId);
            return dalPosts.Select(p => p.ToBLL());
        }

        public IEnumerable<Post> GetPostsByProjectManager(Guid projectId)
        {
            return _dalService.GetPostsByProjectManager(projectId).Select(p => p.ToBLL());
        }

        public void UpdatePost(Guid postId, Guid employeeId, string content)
        {
            _dalService.UpdatePost(postId, employeeId, content);
        }
    }
}
