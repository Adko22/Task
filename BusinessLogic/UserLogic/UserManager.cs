using DataAccess.Models;
using DataAccess.Repositories.UserRepository;

namespace BusinessLogic.UserLogic
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page and pageSize must be greater than zero.");
            }


            return await _userRepository.GetUsersAsync(page, pageSize);
        }

        public async Task<int> TotalUserCountAsync()
        {
            return await _userRepository.TotalUserCountAsync();
        }
    }
}
