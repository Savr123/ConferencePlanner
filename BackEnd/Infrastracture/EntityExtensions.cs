
namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static ConferenceDTO.SessionResponse MapSessionResponse(this Session session) =>
            new ConferenceDTO.SessionResponse()
            {
                id = session.id,
                Title = session.Title,
                EndTime = session.EndTime,
                StartTime = session.StartTime,
                Speakers = session.SessionsSpeakers?.Select(
                    ss => new ConferenceDTO.Speaker
                    {
                        Id = ss.SpeakerId,
                        Name = ss.Speaker.Name
                    }).ToList() ?? new(),
                TrackId = session.TrackId,
                Track = new ConferenceDTO.Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session?.Track?.Name,
                },
                Abstract = session?.Abstract
            };

        public static ConferenceDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new ConferenceDTO.SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new ConferenceDTO.Session
                        {
                            id = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList() ?? new()  
            };
        public static ConferenceDTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new ConferenceDTO.AttendeeResponse
            {
                Email = attendee.Email,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Sessions = attendee.SessionsAttendees?.Select(
                    ss =>
                    new ConferenceDTO.Session
                    {
                        id = ss.SessionId,
                        Title = ss.Session.Title,
                        StartTime = ss.Session.StartTime,
                        EndTime = ss.Session.EndTime
                    }).ToList() ?? new()
            };
    }
}
