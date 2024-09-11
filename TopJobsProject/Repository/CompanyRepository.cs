using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TopJobsProject.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TopJobsContext _context;

        public CompanyRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            try
            {
                return await _context.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving companies: {ex.Message}", ex);
            }
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            try
            {
                return await _context.Companies.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error retrieving company with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error adding new company: {ex.Message}", ex);
            }
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            try
            {
                _context.Companies.Update(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error updating company with ID {company.CompanyId}: {ex.Message}", ex);
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if (company != null)
                {
                    _context.Companies.Remove(company);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Company with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception($"Error deleting company with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
