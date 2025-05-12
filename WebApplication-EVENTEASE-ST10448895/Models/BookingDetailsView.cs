using System;

namespace WebApplication_EVENTEASE_ST10448895.Models
{
    public class BookingDetailsView
    {
        public int Booking_ID { get; set; }
        public DateTime Booking_Date { get; set; }


        public int Venue_ID { get; set; }
        public string Venue_Name { get; set; }
        public string Locations { get; set; }

        public int Event_ID { get; set; }
        public string Event_Name { get; set; }
        public string Descriptions { get; set; }
    }
}
