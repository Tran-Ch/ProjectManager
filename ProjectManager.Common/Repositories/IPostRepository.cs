using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IPostRepository<TPost>
    {
        IEnumerable<TPost> GetPostsByProjectManage(Guid projectId);
        IEnumerable<TPost> GetPostsByEmployeeId(Guid employeeId);
        public void AddPost(TPost post);
        public void UpdatePost(TPost post);
    }
}
