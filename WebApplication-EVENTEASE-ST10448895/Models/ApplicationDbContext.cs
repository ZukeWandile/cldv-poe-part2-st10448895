using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WebApplication_EVENTEASE_ST10448895.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<Bookings> Bookings { get; set; }

        public DbSet<EventS> EventS { get; set; }

        public DbSet<Venue> Venue { get; set; }

        public DbSet<BookingDetailsView> BookingDetailsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookingDetailsView>()
                .HasNoKey()
                .ToView("vwBookingDetails");
        }
    }
}
