using BackEnd.Data;
using ConferenceDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SearchResult>>> Search(SearchTerm term)
        {
            var query = term.Query;
            var sessionResult = await _context.Sessions.Include(s => s.Track)
                                                     .Include(s => s.SessionSpeakers)
                                                     .ThenInclude(ss => ss.Speaker)
                                                     .Where(s =>
                                                                s.Title!.Contains(query) ||
                                                                s.Track!.Name!.Contains(query))
                                                     .ToListAsync();

            var speakerResult = await _context.Speakers.Include(s => s.SessionSpeakers)
                                                 .ThenInclude(ss => ss.Session)
                                                 .Where(s =>
                                                            s.Name!.Contains(query) ||
                                                            s.Bio!.Contains(query) ||
                                                            s.WebSite!.Contains(query))
                                                 .ToListAsync();
            var result = sessionResult.Select(s => new SearchResult
            {
                Type = SearchResultType.Session,
                Session = s.MapSessionResponse()
            })
            .Concat(speakerResult.Select(s => new SearchResult
            {
                Type = SearchResultType.Speaker,
                Speaker = s.MapSpeakerResponse()
            }));

            return result.ToList();
        }
    }
}
