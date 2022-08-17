using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Attendee
    {
        public int id { get; set; }
        [Required]
        [StringLength(200)]
        public virtual string? FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string? LastName { get; set; }

        [Required]
        [StringLength (200)]
        public virtual string? UserName { get; set; }

        [Required]
        [StringLength(256)]
        public virtual string? Email { get; set; }
    }
}
