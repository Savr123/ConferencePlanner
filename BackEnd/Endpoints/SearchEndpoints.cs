using BackEnd.Data;
using ConferenceDTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints
{
    public static class SearchEndpoints
    {
        public static void MapSearchEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Search/{term}", async (string term, ApplicationDbContext db) =>
            {
                var sessionResult = await db.Sessions.Include(s => s.Track)
                                                     .Include(s => s.SessionsSpeakers)
                                                     .ThenInclude(ss => ss.Speaker)
                                                     .Where(s =>
                                                                s.Title!.Contains(term) ||
                                                                s.Track!.Name!.Contains(term))
                                                     .ToListAsync();

                var speakerResult = await db.Speakers.Include(s => s.SessionSpeakers)
                                                     .ThenInclude(ss => ss.Session)
                                                     .Where(s =>
                                                                s.Name!.Contains(term) ||
                                                                s.Bio!.Contains(term) ||
                                                                s.WebSite!.Contains(term))
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

                return result is IEnumerable<SearchResult> model
                                    ? Results.Ok(model)
                                    : Results.NotFound();
            })
            .WithTags("Search")
            .WithName("GetSearchResult")
            .Produces<IEnumerable<SearchResult>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
