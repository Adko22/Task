using DataAccess.Models;

namespace BusinessLogic.UserLogic
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize);
        Task<int> TotalUserCountAsync();
    }
}
