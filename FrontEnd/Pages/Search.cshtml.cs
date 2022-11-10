using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using ConferenceDTO;

namespace FrontEnd.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public string Term { get; set; } = string.Empty;
        public List<SearchResult> SearchResults { get; set; } = new();

        public SearchModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync(string term)
        {
            Term = term;
            if (!string.IsNullOrWhiteSpace(term))
            {
                SearchResults = await _apiClient.SearchAsync(term);
            }
        }
    }
}
