using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using ConferenceDTO;

namespace FrontEnd.Pages
{
    public class SessionModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public SessionResponse? Session { get;set; }
        public int? DayOffset { get; set; }

        public SessionModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            Session = await _apiClient.GetSessionAsync(id);
            if(Session == null)
            {
                return RedirectToPage("/index");
            }
            var allSession = await _apiClient.GetSessionsAsync();
            var startDate = allSession.Min(s => s.StartTime?.Date);
            DayOffset = Session.StartTime?.Subtract(startDate ?? DateTimeOffset.MinValue).Days;

            return Page();
        }
    }
}