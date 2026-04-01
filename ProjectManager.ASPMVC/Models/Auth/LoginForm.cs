using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Auth
{
    public class LoginForm
    {
        [DisplayName("Adresse électronique : ")]
        [EmailAddress(ErrorMessage = "L'adresse électronique n'est pas d'un format valide.")]
        [Required(ErrorMessage = "L'adresse électronique est obligatoire.")]
        public string Email { get; set; }

        [DisplayName("Mot de passe : ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&=\-+])[a-zA-Z\d@$!%*?&=\-+]{8,64}$", ErrorMessage = "Le mot de passe ne correspond pas à la sécurité minimale requise.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit avoir au minimum 8 caractères.")]
        [MaxLength(64, ErrorMessage = "Le mot de passe doit avoir au maximum 64 caractères.")]
        public string Password { get; set; }
    }
}
