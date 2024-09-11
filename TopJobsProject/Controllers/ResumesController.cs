using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopJobsProject.Entities;
using TopJobsProject.Repository;

namespace TopJobsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumesController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResumes()
        {
            var resumes = await _resumeRepository.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResumeById(int id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();
            return Ok(resume);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResume(Resume resume)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdResume = await _resumeRepository.AddResumeAsync(resume);
            return CreatedAtAction(nameof(GetResumeById), new { id = createdResume.ResumeId }, createdResume);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResume(int id, Resume resume)
        {
            if (id != resume.ResumeId) return BadRequest();

            var updatedResume = await _resumeRepository.UpdateResumeAsync(resume);
            if (updatedResume == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();

            await _resumeRepository.DeleteResumeAsync(id);
            return NoContent();
        }
    }
}
