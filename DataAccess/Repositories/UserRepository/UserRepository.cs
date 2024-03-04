using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskContext _context;

        public UserRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<int> TotalUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize)
        {
            IQueryable<User> users = _context.Users;

            users = users.Skip((page - 1) * pageSize).Take(pageSize);

            return await users.ToListAsync();
        }
    }
}
