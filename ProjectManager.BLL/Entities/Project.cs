using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Project
    {
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public Guid ProjectManagerId { get; private set; }

        public Project(Guid projectId, string name, string description, DateTime creationDate, Guid projectManagerId)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            CreationDate = creationDate;
            ProjectManagerId = projectManagerId;
        }
    }
}
