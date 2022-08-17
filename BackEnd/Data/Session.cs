using System;
using System.Collections;
using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Session: ConferenceDTO.Session
    {
        public virtual ICollection<SessionSpeaker> SessionsSpeaker { get; set; } = null;
        public virtual ICollection<SessionAttendee> SessionsAttendee { get; set; } = null;

        public Track Track { get; set; } = null!;
    }
}
