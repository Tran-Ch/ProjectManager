using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class CreateForm
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
