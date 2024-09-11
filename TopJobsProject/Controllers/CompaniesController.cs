using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopJobsProject.Entities;
using TopJobsProject.Repository;
using System.Threading.Tasks;

namespace TopJobsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetAllCompaniesAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByIdAsync(id);
                if (company == null) return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var createdCompany = await _companyRepository.AddCompanyAsync(company);
                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.CompanyId }, createdCompany);
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new company record");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            try
            {
                if (id != company.CompanyId) return BadRequest();

                var updatedCompany = await _companyRepository.UpdateCompanyAsync(company);
                if (updatedCompany == null) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating company record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByIdAsync(id);
                if (company == null) return NotFound();

                await _companyRepository.DeleteCompanyAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting company record");
            }
        }
    }
}
