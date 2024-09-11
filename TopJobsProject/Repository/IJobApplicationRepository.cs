using TopJobsProject.Entities;

namespace TopJobsProject.Repository
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllJobApplicationsAsync();
        Task<JobApplication> GetJobApplicationByIdAsync(int id);
        Task<JobApplication> AddJobApplicationAsync(JobApplication jobApplication);
        Task<JobApplication> UpdateJobApplicationAsync(JobApplication jobApplication);
        Task DeleteJobApplicationAsync(int id);
        Task<IEnumerable<JobApplication>> GetJobApplicationsByUserIdAsync(int userId);
        Task<IEnumerable<JobApplication>> GetJobApplicationsByJobIdAsync(int jobId);

        Task<IEnumerable<JobApplication>> GetJobApplicationsByEmployerIdAsync(int employerId);
    }
}
