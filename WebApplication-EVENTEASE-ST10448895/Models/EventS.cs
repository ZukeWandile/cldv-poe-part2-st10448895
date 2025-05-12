using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_EVENTEASE_ST10448895.Models
{
    public class EventS
    {
        [Key]
        public int Event_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Event_Name { get; set; }

        [Required]
        public DateTime Event_Date { get; set; }

        [Required]
        [StringLength(250)]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "Venue selection is required.")]
        public int? Venue_ID { get; set; }

        [ForeignKey("Venue_ID")]
        public Venue? Venue { get; set; } // allow nulls

    }
}
