namespace ProjectManager.ASPMVC.Models.Project
{
    public class ListItemViewModel
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int MemberCount { get; set; }
    }
}
