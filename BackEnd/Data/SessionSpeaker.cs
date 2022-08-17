namespace BackEnd.Data
{
    public class SessionSpeaker
    {
        public int Id { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = null!;
    }
}
