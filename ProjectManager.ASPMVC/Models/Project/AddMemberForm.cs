using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class AddMemberForm
    {
        [Required]
        public Guid ProjectId { get; set; }

        [DisplayName("Employé : ")]
        [Required(ErrorMessage = "L'employé doit être sélectionné.")]
        public Guid EmployeeId { get; set; }
    }
}
