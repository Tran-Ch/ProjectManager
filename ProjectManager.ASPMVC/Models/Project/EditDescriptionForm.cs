using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class EditDescriptionForm
    {
        [Required]
        public Guid ProjectId { get; set; }

        [DisplayName("Description : ")]
        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; }
    }
}
