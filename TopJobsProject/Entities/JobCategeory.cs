using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TopJobsProject.Entities
{
    public class JobCategory
    {
        [Key]
        public int JobCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }


        [JsonIgnore]
        public ICollection<Job>? Jobs { get; set; }
    }
}
