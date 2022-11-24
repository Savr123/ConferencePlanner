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
        public bool IsInPersonalAgenda {get; set;}

        public SessionModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Session = await _apiClient.GetSessionAsync(id);
            if(Session == null)
            {
                return RedirectToPage("/index");
            }
            if (User.Identity.IsAuthenticated)
            {
                var session = await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);
                IsInPersonalAgenda = session.Any(s => s.id == id);
            }
            var allSession = await _apiClient.GetSessionsAsync();
            var startDate = allSession.Min(s => s.StartTime?.Date);
            DayOffset = Session.StartTime?.Subtract(startDate ?? DateTimeOffset.MinValue).Days;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int sessionId)
        {
            await _apiClient.AddSessionToAttendeeAsync(User.Identity.Name, sessionId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int sessionId)
        {
            await _apiClient.RemoveSessionFromAttendeeAsync(User.Identity.Name, sessionId);

            return RedirectToPage();
        }
    }
}
