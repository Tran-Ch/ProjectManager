using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class EditDescriptionForm
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
