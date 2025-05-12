using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_EVENTEASE_ST10448895.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_EVENTEASE_ST10448895.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var bookings = from b in _context.BookingDetailsView
                           select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b =>
                    b.Booking_ID.ToString().Contains(searchString) ||
                    b.Event_Name.Contains(searchString));
            }

            return View(await bookings.ToListAsync());
        }
    }
}
