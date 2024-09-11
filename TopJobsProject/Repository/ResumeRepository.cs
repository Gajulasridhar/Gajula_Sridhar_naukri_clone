using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;

namespace TopJobsProject.Repository
{
   
    public class ResumeRepository : IResumeRepository
    {
        private readonly TopJobsContext _context;

        public ResumeRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resume>> GetAllResumesAsync()
        {
            return await _context.Resumes.Include(r => r.User).ToListAsync();
        }

        public async Task<Resume> GetResumeByIdAsync(int id)
        {
            return await _context.Resumes.Include(r => r.User).FirstOrDefaultAsync(r => r.ResumeId == id);
        }

        public async Task<Resume> AddResumeAsync(Resume resume)
        {
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        public async Task<Resume> UpdateResumeAsync(Resume resume)
        {
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        public async Task DeleteResumeAsync(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            if (resume != null)
            {
                _context.Resumes.Remove(resume);
                await _context.SaveChangesAsync();
            }
        }
    }

}
