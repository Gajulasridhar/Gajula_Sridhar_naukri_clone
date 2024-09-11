using TopJobsProject.Entities;

namespace TopJobsProject.Repository
{
    public interface IResumeRepository
    {
        Task<IEnumerable<Resume>> GetAllResumesAsync();
        Task<Resume> GetResumeByIdAsync(int id);
        Task<Resume> AddResumeAsync(Resume resume);
        Task<Resume> UpdateResumeAsync(Resume resume);
        Task DeleteResumeAsync(int id);
    }
}
