using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Session
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string? Title { get; set; }

        [StringLength(4000)]
        public virtual string? Abstract { get; set; }

        public virtual DateTimeOffset? StartTime { get; set; }

        public virtual DateTimeOffset? EndTime { get; set; }

        // Bonus points to those who can figure out why this is written this way
        //we're calculating duration time, cuz collecting it as a new waypoint is not smart, we can face errors while changing startTime and forgetting to change duration
        //but while we use method to calculate it so then there's no need to store value for session duration
        public TimeSpan Duration =>
            EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ??
            TimeSpan.Zero;

        public int? TrackId { get; set; }
    }
}
