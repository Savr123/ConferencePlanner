﻿namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static ConferenceDTO.SpeakerResponse MapSeakerResponse(this Speaker speaker) =>
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
                            id = ss.Id,
                            Title = ss.Session.Title
                        })
                    .ToList() ?? new()
            };
    }
}