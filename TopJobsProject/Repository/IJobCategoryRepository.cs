using TopJobsProject.Entities;

namespace TopJobsProject.Repository
{
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategoriesAsync();
        Task<JobCategory> GetJobCategoryByIdAsync(int id);
        Task<JobCategory> AddJobCategoryAsync(JobCategory jobCategory);
        Task<JobCategory> UpdateJobCategoryAsync(JobCategory jobCategory);
        Task DeleteJobCategoryAsync(int id);
    }
}
