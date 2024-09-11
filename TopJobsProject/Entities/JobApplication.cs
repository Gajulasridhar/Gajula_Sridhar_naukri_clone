using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopJobsProject.Entities
{
    public class JobApplication
    {
        [Key]
        public int JobApplicationId { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]

        public Job Job { get; set; }

        public string Status { get; set; } = "Pending";

    }
}
