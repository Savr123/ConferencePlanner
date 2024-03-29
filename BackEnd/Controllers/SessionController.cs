﻿using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using ConferenceDTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SessionResponse>>> Get()
        {
            var sessions = await _context.Sessions.AsNoTracking()
                                             .Include(s => s.Track)
                                             .Include(s => s.SessionSpeakers)
                                                .ThenInclude(ss => ss.Speaker)
                                             .Select(m => m.MapSessionResponse())
                                             .ToListAsync();
            return sessions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponse>> Get(int id)
        {
            var session = await _context.Sessions.AsNoTracking()
                                            .Include(s => s.Track)
                                            .Include(s => s.SessionSpeakers)
                                                .ThenInclude(ss => ss.Speaker)
                                            .SingleOrDefaultAsync(s => s.id == id);

            if (session == null)
            {
                return NotFound();
            }

            return session.MapSessionResponse();
        }

        [HttpPost]
        public async Task<ActionResult<SessionResponse>> Post(ConferenceDTO.Session input)
        {
            var session = new Data.Session
            {
                Title = input.Title,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Abstract = input.Abstract,
                TrackId = input.TrackId
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var result = session.MapSessionResponse();

            return CreatedAtAction(nameof(Get), new { id = result.id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ConferenceDTO.Session input)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            session.id = input.id;
            session.Title = input.Title;
            session.Abstract = input.Abstract;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;
            session.TrackId = input.TrackId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionResponse>> Delete(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return session.MapSessionResponse();
        }


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] ConferenceFormat format, IFormFile file)
        {
            var loader = GetLoader(format);

            using (var stream = file.OpenReadStream())
            {
                await loader.LoadDataAsync(stream, _context);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private static DataLoader GetLoader(ConferenceFormat format)
        {
            if (format == ConferenceFormat.Sessionize)
            {
                return new SessionizeLoader();
            }
            return new DevIntersectionLoader();
        }

        public enum ConferenceFormat
        {
            Sessionize,
            DevIntersections
        }

    }
}
