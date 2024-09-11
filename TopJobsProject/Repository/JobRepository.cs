using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopJobsProject.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly TopJobsContext _context;

        public JobRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            try
            {
                return await _context.Jobs
                    .Include(j => j.Company)
                    .Include(j => j.JobCategory)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error retrieving all jobs: {ex.Message}", ex);
            }
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            try
            {
                return await _context.Jobs
                    .Include(j => j.Company)
                    .Include(j => j.JobCategory)
                    .FirstOrDefaultAsync(j => j.JobId == id);
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error retrieving job with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Job> AddJobAsync(Job job)
        {
            try
            {
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return job;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error adding job: {ex.Message}", ex);
            }
        }

        public async Task<Job> UpdateJobAsync(Job job)
        {
            try
            {
                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();
                return job;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error updating job with ID {job.JobId}: {ex.Message}", ex);
            }
        }

        public async Task DeleteJobAsync(int id)
        {
            try
            {
                var job = await _context.Jobs.FindAsync(id);
                if (job != null)
                {
                    _context.Jobs.Remove(job);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Job with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error deleting job with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Job>> SearchJobsAsync(string? jobTitle, int? companyId, int? jobCategoryId, DateTime? postedAfter)
        {
            try
            {
                // Start with a basic query
                var query = _context.Jobs.AsQueryable();

                // Apply filters based on provided parameters
                if (!string.IsNullOrEmpty(jobTitle))
                {
                    query = query.Where(j => j.JobTitle.Contains(jobTitle));
                }

                if (companyId.HasValue)
                {
                    query = query.Where(j => j.CompanyId == companyId.Value);
                }

                if (jobCategoryId.HasValue)
                {
                    query = query.Where(j => j.JobCategoryId == jobCategoryId.Value);
                }

                if (postedAfter.HasValue)
                {
                    query = query.Where(j => j.PostedDate >= postedAfter.Value);
                }

                // Include related entities for detailed search results
                query = query.Include(j => j.Company).Include(j => j.JobCategory);

                // Execute query and return results
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error searching for jobs: {ex.Message}", ex);
            }
        }
    }
}
