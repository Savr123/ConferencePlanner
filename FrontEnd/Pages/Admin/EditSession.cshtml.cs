using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using ConferenceDTO;

namespace FrontEnd.Pages.Admin
{
    public class EditSessionModel : PageModel
    {
        [BindProperty]
        public Session Session { get; set; }
        private readonly IApiClient _apiClient;
        [TempData]
        public string Message { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        public EditSessionModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync(int id)
        {
            var session = await _apiClient.GetSessionAsync(id);
            Session = new Session
            {
                id = session.id,
                TrackId = session.TrackId,
                Title = session.Title,
                Abstract = session.Abstract,
                EndTime = session.EndTime,
                StartTime = session.StartTime,
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Message = "Session updated successfully!";
            await _apiClient.PutSessionAsync(Session);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await _apiClient.GetSessionAsync(id);
            if (session != null)
            {
                await _apiClient.DeleteSessionAsync(id);
            }
            Message = "Session deleted successfully!";

            return Page();
        }
    }
}
