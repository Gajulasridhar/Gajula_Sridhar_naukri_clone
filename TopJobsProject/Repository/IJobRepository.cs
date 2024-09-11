using TopJobsProject.Entities;

namespace TopJobsProject.Repository
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(int id);
        Task<Job> AddJobAsync(Job job);
        Task<Job> UpdateJobAsync(Job job);
        Task DeleteJobAsync(int id);

        Task<IEnumerable<Job>> SearchJobsAsync(string? jobTitle, int? companyId, int? jobCategoryId, DateTime? postedAfter);

    }
}
