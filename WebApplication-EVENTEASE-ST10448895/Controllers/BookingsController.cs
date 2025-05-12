using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_EVENTEASE_ST10448895.Models;

namespace WebApplication_EVENTEASE_ST10448895.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string searchString)
        {
            var bookings = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventS)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b =>
                    b.EventS.Event_Name.Contains(searchString) ||
                    b.Venue.Venue_Name.Contains(searchString) ||
                    b.Booking_ID.ToString().Contains(searchString));
            }

            return View(await bookings.ToListAsync());
        }


        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventS)
                .FirstOrDefaultAsync(m => m.Booking_ID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewBag.Venue_ID = new SelectList(_context.Venue, "Venue_ID", "Venue_Name");
            ViewBag.Event_ID = new SelectList(_context.EventS, "Event_ID", "Event_Name");
            return View();
        }


        // POST: Bookings/Create
        [HttpPost]
        public async Task<IActionResult> Create(Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                if (bookings.Venue_ID == null || bookings.Event_ID == 0)
                {
                    ModelState.AddModelError("", "Please select both Venue and Event.");
                }
                else
                {
                    var selectedEventDate = await _context.EventS
                        .Where(e => e.Event_ID == bookings.Event_ID)
                        .Select(e => e.Event_Date)
                        .FirstOrDefaultAsync();

                    bool isDoubleBooked = await _context.Bookings
                        .Include(b => b.EventS)
                        .AnyAsync(b =>
                            b.Venue_ID == bookings.Venue_ID &&
                            b.EventS.Event_Date == selectedEventDate);

                    if (isDoubleBooked)
                    {
                        ModelState.AddModelError("", "This venue is already booked for the selected date and time.");
                    }
                    else
                    {
                        _context.Bookings.Add(bookings);
                        await _context.SaveChangesAsync();

                        
                        TempData["SuccessMessage"] = "Booking created successfully!";
                        return RedirectToAction(nameof(Create));
                    }
                }
            }

            ViewBag.Venue_ID = new SelectList(_context.Venue, "Venue_ID", "Venue_Name", bookings.Venue_ID);
            ViewBag.Event_ID = new SelectList(_context.EventS, "Event_ID", "Event_Name", bookings.Event_ID);
            return View(bookings);
        }



        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventS)
                .FirstOrDefaultAsync(b => b.Booking_ID == id);

            if (bookings == null)
            {
                return NotFound();
            }

            ViewBag.Venue_ID = new SelectList(_context.Venue, "Venue_ID", "Venue_Name", bookings.Venue_ID);
            ViewBag.Event_ID = new SelectList(_context.EventS, "Event_ID", "Event_Name", bookings.Event_ID);

            return View(bookings);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bookings bookings)
        {
            if (id != bookings.Booking_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var selectedEventDate = await _context.EventS
                    .Where(e => e.Event_ID == bookings.Event_ID)
                    .Select(e => e.Event_Date)
                    .FirstOrDefaultAsync();

                bool isDoubleBooked = await _context.Bookings
                    .Include(b => b.EventS)
                    .AnyAsync(b =>
                        b.Venue_ID == bookings.Venue_ID &&
                        b.EventS.Event_Date == selectedEventDate &&
                        b.Booking_ID != bookings.Booking_ID); // exclude current booking

                if (isDoubleBooked)
                {
                    ModelState.AddModelError(string.Empty, "This venue is already booked for the selected date and time.");
                }
                else
                {
                    try
                    {
                        _context.Update(bookings);
                        await _context.SaveChangesAsync();

                        
                        TempData["SuccessMessage"] = "Booking updated successfully!";

                        return RedirectToAction(nameof(Edit), new { id = bookings.Booking_ID }); // Redirect to same edit page
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_context.Bookings.Any(e => e.Booking_ID == bookings.Booking_ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            ViewBag.Venue_ID = new SelectList(_context.Venue, "Venue_ID", "Venue_Name", bookings.Venue_ID);
            ViewBag.Event_ID = new SelectList(_context.EventS, "Event_ID", "Event_Name", bookings.Event_ID);
            return View(bookings);
        }




        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventS)
                .FirstOrDefaultAsync(m => m.Booking_ID == id);

            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
                await _context.SaveChangesAsync();

               
                TempData["SuccessMessage"] = "Booking deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }


        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Booking_ID == id);
        }
    }
}
