using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IPostRepository<TPost>
    {
        IEnumerable<TPost> GetPostsByProjectManager(Guid projectId);
        IEnumerable<TPost> GetPostsByEmployee(Guid projectId, Guid employeeId);
        Guid AddPost(TPost post);
        void UpdatePost(Guid postId, Guid employeeId, string content);
    }
}
