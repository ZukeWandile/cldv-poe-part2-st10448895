using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_EVENTEASE_ST10448895.Models
{
    public class Bookings
    {
        [Key]
        public int Booking_ID { get; set; }

        [Required(ErrorMessage = "Please select a venue.")]
        public int Venue_ID { get; set; }

        [ForeignKey("Venue_ID")]
        public Venue? Venue { get; set; }

        [Required(ErrorMessage = "Please select an event.")]
        public int Event_ID { get; set; }

        [ForeignKey("Event_ID")]
        public EventS? EventS { get; set; }

        [Required(ErrorMessage = "Please enter a booking date.")]
        [DataType(DataType.Date)]
        public DateTime Booking_Date { get; set; }
    }
}
