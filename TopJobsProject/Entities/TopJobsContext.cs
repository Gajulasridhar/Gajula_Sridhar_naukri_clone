using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TopJobsProject.Entities
{
    public class TopJobsContext: DbContext
    {
        private IConfiguration configuration;

        public TopJobsContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Data Source=GOUTHAMS\\SQLEXPRESS;Initial Catalog=ECommerce;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TopJobsConnection"));

        }
    }
}
