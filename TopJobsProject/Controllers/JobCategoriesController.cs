using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopJobsProject.Entities;
using TopJobsProject.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TopJobsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobCategoriesController(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobCategories()
        {
            try
            {
                var jobCategories = await _jobCategoryRepository.GetAllJobCategoriesAsync();
                return Ok(jobCategories);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving job categories.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobCategoryById(int id)
        {
            try
            {
                var jobCategory = await _jobCategoryRepository.GetJobCategoryByIdAsync(id);
                if (jobCategory == null) return NotFound();
                return Ok(jobCategory);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the job category.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateJobCategory(JobCategory jobCategory)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var createdJobCategory = await _jobCategoryRepository.AddJobCategoryAsync(jobCategory);
                return CreatedAtAction(nameof(GetJobCategoryById), new { id = createdJobCategory.JobCategoryId }, createdJobCategory);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the job category.");
            }
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateJobCategory(int id, JobCategory jobCategory)
        {
            try
            {
                if (id != jobCategory.JobCategoryId) return BadRequest();

                var updatedJobCategory = await _jobCategoryRepository.UpdateJobCategoryAsync(jobCategory);
                if (updatedJobCategory == null) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the job category.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteJobCategory(int id)
        {
            try
            {
                var jobCategory = await _jobCategoryRepository.GetJobCategoryByIdAsync(id);
                if (jobCategory == null) return NotFound();

                await _jobCategoryRepository.DeleteJobCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the job category.");
            }
        }
    }
}
