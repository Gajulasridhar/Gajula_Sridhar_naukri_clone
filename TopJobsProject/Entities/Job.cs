using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TopJobsProject.Entities
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [MaxLength(200)]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        [Required]
        public int JobCategoryId { get; set; }

        [ForeignKey("JobCategoryId")]
        public JobCategory? JobCategory { get; set; }

        [Required]
        public int UserId { get; set; }  // Foreign key to the User (Employer) who posted the job

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? Employer { get; set; }  // The user who posted the job

        [JsonIgnore]
        public ICollection<JobApplication>? JobApplications { get; set; }
    }
}
