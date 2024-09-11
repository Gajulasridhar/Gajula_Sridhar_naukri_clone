using Microsoft.AspNetCore.Mvc;
using TopJobsProject.Entities;
using TopJobsProject.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace TopJobsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobsController(
            IJobRepository jobRepository,
            ICompanyRepository companyRepository,
            IJobCategoryRepository jobCategoryRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                var jobs = await _jobRepository.GetAllJobsAsync();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving jobs.");
            }
        }

        [HttpGet, Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchJobs(
            [FromQuery] string? jobTitle,
            [FromQuery] int? companyId,
            [FromQuery] int? jobCategoryId,
            [FromQuery] DateTime? postedAfter)
        {
            try
            {
                var jobs = await _jobRepository.SearchJobsAsync(jobTitle, companyId, jobCategoryId, postedAfter);

                if (jobs == null || !jobs.Any())
                {
                    return NotFound("No jobs found matching the search criteria.");
                }

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while searching for jobs.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            try
            {
                var job = await _jobRepository.GetJobByIdAsync(id);
                if (job == null) return NotFound();
                return Ok(job);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the job.");
            }
        }

        [HttpPost]
       
        public async Task<IActionResult> CreateJob(Job job)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                // Fetch company details based on CompanyId
                if (job.CompanyId != 0)
                {
                    var company = await _companyRepository.GetCompanyByIdAsync(job.CompanyId);
                    if (company == null)
                    {
                        return BadRequest("The specified company does not exist.");
                    }
                    job.Company = company;
                }

                // Fetch category details based on JobCategoryId
                if (job.JobCategoryId != 0)
                {
                    var category = await _jobCategoryRepository.GetJobCategoryByIdAsync(job.JobCategoryId);
                    if (category == null)
                    {
                        return BadRequest("The specified category does not exist.");
                    }
                    job.JobCategory = category;
                }

                var createdJob = await _jobRepository.AddJobAsync(job);
                return CreatedAtAction(nameof(GetJobById), new { id = createdJob.JobId }, createdJob);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the job.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateJob(int id, Job job)
        {
            try
            {
                if (id != job.JobId) return BadRequest();

                // Fetch company details based on CompanyId
                if (job.CompanyId != 0)
                {
                    var company = await _companyRepository.GetCompanyByIdAsync(job.CompanyId);
                    if (company == null)
                    {
                        return BadRequest("The specified company does not exist.");
                    }
                    job.Company = company;
                }

                // Fetch category details based on JobCategoryId
                if (job.JobCategoryId != 0)
                {
                    var category = await _jobCategoryRepository.GetJobCategoryByIdAsync(job.JobCategoryId);
                    if (category == null)
                    {
                        return BadRequest("The specified category does not exist.");
                    }
                    job.JobCategory = category;
                }

                var updatedJob = await _jobRepository.UpdateJobAsync(job);
                if (updatedJob == null) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the job.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                var job = await _jobRepository.GetJobByIdAsync(id);
                if (job == null) return NotFound();

                await _jobRepository.DeleteJobAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the job.");
            }
        }
    }
}
