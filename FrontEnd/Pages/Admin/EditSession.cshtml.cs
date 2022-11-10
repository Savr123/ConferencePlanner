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
        public EditSessionModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async void OnGet(int id)
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
            await _apiClient.PutSessionAsync(Session);

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var session = await _apiClient.GetSessionAsync(id);
            if (session != null)
            {
                await _apiClient.DeleteSessionAsync(id);
            }

            return Page();
        }
    }
}
