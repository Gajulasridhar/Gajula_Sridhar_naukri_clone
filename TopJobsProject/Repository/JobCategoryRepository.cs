using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TopJobsProject.Repository
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly TopJobsContext _context;

        public JobCategoryRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategoriesAsync()
        {
            try
            {
                return await _context.JobCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving all job categories: {ex.Message}", ex);
            }
        }

        public async Task<JobCategory> GetJobCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.JobCategories.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving job category with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<JobCategory> AddJobCategoryAsync(JobCategory jobCategory)
        {
            try
            {
                _context.JobCategories.Add(jobCategory);
                await _context.SaveChangesAsync();
                return jobCategory;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error adding job category: {ex.Message}", ex);
            }
        }

        public async Task<JobCategory> UpdateJobCategoryAsync(JobCategory jobCategory)
        {
            try
            {
                _context.JobCategories.Update(jobCategory);
                await _context.SaveChangesAsync();
                return jobCategory;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error updating job category with ID {jobCategory.JobCategoryId}: {ex.Message}", ex);
            }
        }

        public async Task DeleteJobCategoryAsync(int id)
        {
            try
            {
                var jobCategory = await _context.JobCategories.FindAsync(id);
                if (jobCategory != null)
                {
                    _context.JobCategories.Remove(jobCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Job category with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error deleting job category with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
