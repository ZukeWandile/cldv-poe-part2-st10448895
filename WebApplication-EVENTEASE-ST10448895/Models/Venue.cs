using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_EVENTEASE_ST10448895.Models
{
    public class Venue
    {
        [Key]
        public int Venue_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Venue_Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Locations { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; }

      
        public string? ImageUrl { get; set; }

        [NotMapped]
    
        public IFormFile? ImageFile { get; set; }

    }
}
