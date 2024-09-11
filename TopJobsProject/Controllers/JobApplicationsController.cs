using Microsoft.AspNetCore.Mvc;
using TopJobsProject.Entities;
using TopJobsProject.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace TopJobsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public JobApplicationsController(
            IJobApplicationRepository jobApplicationRepository,
            IJobRepository jobRepository,
            IUserRepository userRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        [HttpGet, Route("GetAllJobApplications")]
        [Authorize(Roles ="Employer")]
        public async Task<IActionResult> GetAllJobApplications()
        {
            try
            {
                var jobApplications = await _jobApplicationRepository.GetAllJobApplicationsAsync();
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving job applications");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetJobApplicationById(int id)
        {
            try
            {
                var jobApplication = await _jobApplicationRepository.GetJobApplicationByIdAsync(id);
                if (jobApplication == null) return NotFound();
                return Ok(jobApplication);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving the job application");
            }
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateJobApplication(JobApplication jobApplication)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userRepository.GetUserByIdAsync(jobApplication.UserId);
                if (user == null)
                {
                    return BadRequest("The specified user does not exist.");
                }

                var job = await _jobRepository.GetJobByIdAsync(jobApplication.JobId);
                if (job == null)
                {
                    return BadRequest("The specified job does not exist.");
                }

                jobApplication.User = user;
                jobApplication.Job = job;
                jobApplication.Job.Company = job.Company;
                jobApplication.Job.JobCategory = job.JobCategory;

                var createdJobApplication = await _jobApplicationRepository.AddJobApplicationAsync(jobApplication);
                return CreatedAtAction(nameof(GetJobApplicationById), new { id = createdJobApplication.JobApplicationId }, createdJobApplication);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating job application");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateJobApplication(int id, JobApplication jobApplication)
        {
            try
            {
                if (id != jobApplication.JobApplicationId) return BadRequest();

                var updatedJobApplication = await _jobApplicationRepository.UpdateJobApplicationAsync(jobApplication);
                if (updatedJobApplication == null) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating job application");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            try
            {
                var jobApplication = await _jobApplicationRepository.GetJobApplicationByIdAsync(id);
                if (jobApplication == null) return NotFound();

                await _jobApplicationRepository.DeleteJobApplicationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting job application");
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateJobApplicationStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newStatus))
                {
                    return BadRequest("Status cannot be empty.");
                }

                var jobApplication = await _jobApplicationRepository.GetJobApplicationByIdAsync(id);
                if (jobApplication == null) return NotFound();

                jobApplication.Status = newStatus;
                await _jobApplicationRepository.UpdateJobApplicationAsync(jobApplication);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating job application status");
            }
        }

        [HttpGet("user/{userId}")]
        
        public async Task<IActionResult> GetJobApplicationsByUserId(int userId)
        {
            try
            {
                var jobApplications = await _jobApplicationRepository.GetJobApplicationsByUserIdAsync(userId);
                if (jobApplications == null || !jobApplications.Any())
                    return NotFound("No job applications found for this user.");

                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving job applications by user ID");
            }
        }

        [HttpGet("job/{jobId}")]
       
        public async Task<ActionResult<IEnumerable<JobApplication>>> GetJobApplicationsByJobId(int jobId)
        {
            try
            {
                var jobApplications = await _jobApplicationRepository.GetJobApplicationsByJobIdAsync(jobId);
                if (jobApplications == null)
                {
                    return NotFound();
                }
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving job applications by job ID");
            }
        }

        [HttpGet("employer/{employerId}")]
        public async Task<IActionResult> GetJobApplicationsByEmployerId(int employerId)
        {
            try
            {
                var jobApplications = await _jobApplicationRepository.GetJobApplicationsByEmployerIdAsync(employerId);
                if (jobApplications == null || !jobApplications.Any())
                    return NotFound("No job applications found for this employer.");

                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving job applications by employer ID");
            }
        }
    }
}
