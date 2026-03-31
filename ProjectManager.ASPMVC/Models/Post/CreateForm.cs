using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Post
{
    public class CreateForm
    {
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
