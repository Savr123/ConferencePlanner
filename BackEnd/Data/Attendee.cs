namespace BackEnd.Data
{
    public class Attendee: ConferenceDTO.Attendee
    {
        public virtual ICollection<SessionAttendee> SessionsAttendee { get; set; } = null;

    }
}
