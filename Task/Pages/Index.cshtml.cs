using BusinessLogic.TimeLogLogic;
using BusinessLogic.UserLogic;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITimeLogManager _timeLogManager;
        private readonly IUserManager _userManager;

        public IndexModel(ITimeLogManager timeLogManager, IUserManager userManager)
        {
            _timeLogManager = timeLogManager;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; } = 10;
        public int UserCount { get; } = 10;
        public IEnumerable<User> Users { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var totalUsers = await _userManager.TotalUserCountAsync();
            TotalPages = (int)Math.Ceiling((double)totalUsers / PageSize);

            Users = await _userManager.GetUsersAsync(CurrentPage, PageSize);

            return Page();
        }

        public async Task<JsonResult> OnGetChartDataAsync(DateTime? startDate, DateTime? endDate, bool isProject)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var chartData = await _timeLogManager.GetTopEntitiesByHoursAsync(isProject, 10, startDate.Value, endDate.Value);

            return new JsonResult(chartData.Select(d => new { d.EntityName, d.TotalHours,d.EntityId }));
        }


        public async Task<JsonResult> OnGetCompareUserHoursAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now; 

            var totalHours = await _timeLogManager.GetTotalHoursForUserAsync(userId, startDate.Value, endDate.Value);

            return new JsonResult(new { UserId = userId, TotalHours = totalHours });
        }

    }
}
