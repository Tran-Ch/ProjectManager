using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class AddMemberForm
    {
        public Guid ProjectId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
    }
}
