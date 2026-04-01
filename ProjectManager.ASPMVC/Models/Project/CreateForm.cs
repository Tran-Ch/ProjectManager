using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class CreateForm
    {
        [DisplayName("Nom du projet : ")]
        [Required(ErrorMessage = "Le nom du projet est obligatoire.")]
        [MaxLength(256, ErrorMessage = "Le nom du projet ne peut dépasser 256 caractères.")]
        public string Name { get; set; }

        [DisplayName("Description : ")]
        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; }
    }
}
