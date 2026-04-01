using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Post
{
    public class CreateForm
    {
        [Required]
        public Guid ProjectId { get; set; }

        [DisplayName("Sujet : ")]
        [Required(ErrorMessage = "Le sujet est obligatoire.")]
        [MaxLength(256, ErrorMessage = "Le sujet ne peut dépasser 256 caractères.")]
        public string Subject { get; set; }

        [DisplayName("Contenu : ")]
        [Required(ErrorMessage = "Le contenu est obligatoire.")]
        public string Content { get; set; }
    }
}
