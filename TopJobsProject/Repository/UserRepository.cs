using Microsoft.EntityFrameworkCore;
using TopJobsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopJobsProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TopJobsContext _context;

        public UserRepository(TopJobsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Resumes)
                    .Include(u => u.JobApplications)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception("Error retrieving all users", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Resumes)
                    .Include(u => u.JobApplications)
                    .FirstOrDefaultAsync(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error retrieving user with ID {id}", ex);
            }
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception("Error adding user", ex);
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error updating user with ID {user.UserId}", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Error deleting user with ID {id}", ex);
            }
        }

        public User ValidUser(string email, string password)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception("Error validating user", ex);
            }
        }
    }
}
