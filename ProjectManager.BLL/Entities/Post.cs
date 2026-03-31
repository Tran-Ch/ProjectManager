using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Post
    {
        public Guid PostId { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
        public DateTime SendDate { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid ProjectId { get; private set; }

        public Post(Guid postId, string subject, string content, DateTime sendDate, Guid employeeId, Guid projectId)
        {
            PostId = postId;
            Subject = subject;
            Content = content;
            SendDate = sendDate;
            EmployeeId = employeeId;
            ProjectId = projectId;
        }
    }
}