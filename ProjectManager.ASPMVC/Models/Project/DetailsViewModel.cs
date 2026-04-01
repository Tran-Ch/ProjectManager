namespace ProjectManager.ASPMVC.Models.Project
{
    public class DetailsViewModel
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool CanManageProject { get; set; }
        public IEnumerable<ProjectManager.BLL.Entities.Employee> Members { get; set; }
        public IEnumerable<ProjectManager.BLL.Entities.Post> Posts { get; set; }
    }
}
