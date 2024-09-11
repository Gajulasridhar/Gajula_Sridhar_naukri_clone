using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TopJobsProject.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [Required]
        public string Location { get; set; }

        [JsonIgnore]
        public ICollection<Job>? Jobs { get; set; }
    }
}
