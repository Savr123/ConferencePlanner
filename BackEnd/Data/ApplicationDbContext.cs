using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
                .HasIndex(a => a.UserName)
                .IsUnique();

            //Many-to-Many: Session <-> Attendee
            modelBuilder.Entity<SessionAttendee>()
                .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

            //Many-to-many: Speaker <-> Session
            modelBuilder.Entity<SessionSpeaker>()
                .HasKey(ca => new { ca.SessionId, ca.AttendeeId });
        }

        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Track> Tracks => Set<Track>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Speaker> Speakers => Set<Speaker>();

    }
}
