using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TopJobsProject.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public ICollection<JobApplication>? JobApplications { get; set; }

        [JsonIgnore]
        public ICollection<Resume>? Resumes { get; set; }

        // Relation with Jobs posted by the Employer
        [JsonIgnore]
        public ICollection<Job>? PostedJobs { get; set; } // Jobs posted by this user
    }
}
