using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClassRegistrationBack.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ClassContext _context;

        public UserController(ILogger<UserController> logger, ClassContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InstructorDetails(int Id)
        {

            using (var context = _context)
            {

                var instructorsDetails = context.instructors.Where(r => r.Id == Id);
                if (instructorsDetails == null)
                {
                    return NotFound();
                }
                return Ok(instructorsDetails);
            }
        }

        [HttpGet]
        public IActionResult AdressDetails(int Id)
        {

            using (var context = _context)
            {

                var instructorsDetails = context.addresses.Where(a => a.Id == Id);
                if (instructorsDetails == null)
                {
                    return NotFound();
                }
                return Ok(instructorsDetails);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.bookings.Find(id);

            if (booking == null) { 
            
            return NotFound();
            }

            _context.bookings.Remove(booking);
            _context.SaveChanges();

         return NoContent();

        }
    }
    }

