using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopJobsProject.Repository
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly TopJobsContext _context;

        public JobApplicationRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplication>> GetAllJobApplicationsAsync()
        {
            try
            {
                return await _context.JobApplications
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company) // Include Company details
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.JobCategory) // Include JobCategory details
                    .Include(ja => ja.User) // Include User details
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving all job applications: {ex.Message}", ex);
            }
        }

        public async Task<JobApplication> GetJobApplicationByIdAsync(int id)
        {
            try
            {
                return await _context.JobApplications
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company) // Include Company details
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.JobCategory) // Include JobCategory details
                    .Include(ja => ja.User) // Include User details
                    .FirstOrDefaultAsync(ja => ja.JobApplicationId == id);
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving job application with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<JobApplication> AddJobApplicationAsync(JobApplication jobApplication)
        {
            try
            {
                _context.JobApplications.Add(jobApplication);
                await _context.SaveChangesAsync();
                return jobApplication;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error adding job application: {ex.Message}", ex);
            }
        }

        public async Task<JobApplication> UpdateJobApplicationAsync(JobApplication jobApplication)
        {
            try
            {
                _context.JobApplications.Update(jobApplication);
                await _context.SaveChangesAsync();
                return jobApplication;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error updating job application with ID {jobApplication.JobApplicationId}: {ex.Message}", ex);
            }
        }

        public async Task DeleteJobApplicationAsync(int id)
        {
            try
            {
                var jobApplication = await _context.JobApplications.FindAsync(id);
                if (jobApplication != null)
                {
                    _context.JobApplications.Remove(jobApplication);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Job application with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error deleting job application with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsByUserIdAsync(int userId)
        {
            try
            {
                return await _context.JobApplications
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company)
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.JobCategory)
                    .Include(ja => ja.User)
                    .Where(ja => ja.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving job applications for user ID {userId}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsByJobIdAsync(int jobId)
        {
            try
            {
                return await _context.JobApplications
                    .Where(ja => ja.JobId == jobId)
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company)
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.JobCategory)
                    .Include(ja => ja.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving job applications for job ID {jobId}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsByEmployerIdAsync(int employerId)
        {
            try
            {
                return await _context.JobApplications
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company)
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.JobCategory)
                    .Include(ja => ja.User)
                    .Where(ja => ja.Job.UserId == employerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving job applications for employer ID {employerId}: {ex.Message}", ex);
            }
        }
    }
}
