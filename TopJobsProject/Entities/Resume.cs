using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopJobsProject.Entities
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        public string ResumeContent { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
