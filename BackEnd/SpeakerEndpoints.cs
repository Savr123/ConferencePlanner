using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
//using BackEnd.Infrastacure;
namespace BackEnd.Endpoints;

public static class SpeakerEndpoints
{
    public static void MapSpeakerEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Speaker", async (ApplicationDbContext db) =>
        {
            var speaker = await db.Speakers.AsNoTracking()
                                           .Include(s => s.SessionSpeakers)
                                           .ThenInclude(ss => ss.Session)
                                           .Select(s => s.MapSeakerResponse())
                                           .ToListAsync();
            return speaker;
        })
        .WithTags("Speaker").WithName("GetAllSpeakers")
        .Produces<List<Speaker>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Speaker/{id}", async (int id, ApplicationDbContext db) =>
        {
            return await db.Speakers.AsNoTracking()
                                           .Include(s => s.SessionSpeakers)
                                           .ThenInclude(ss => ss.Session)
                                           .SingleOrDefaultAsync(s => s.Id == id)
                                is Speaker model
                                    ? Results.Ok(model.MapSeakerResponse())
                                    : Results.NotFound();
        })
        .WithTags("Speaker").WithName("GetSpeakerById")
        .Produces<Speaker>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
