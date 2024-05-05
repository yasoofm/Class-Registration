using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Gyms(int id)
        {
            var gym = _context.Gyms.Find();

            if (gym == null)
            {
                return NotFound();
            }
            return Ok(_context.Gyms);
        }

        [HttpGet]
        public IEnumerable<Section> GetAll()
        {
            return _context.Sections;
        }

        [HttpPost]
        public IActionResult Book(int userId, int sectionId)
        {
            var user = _context.Users.Find(userId);
            var section = _context.Sections
                .Include(s => s.Bookings)
                .FirstOrDefault(s => s.Id == sectionId);

            if (user != null && section != null)
            {
                if (section.Bookings.Count < 5) 
                {
                    var booking = new Booking
                    {
                        CreateAt = DateTime.UtcNow,
                        Section = section,
                        User = user
                    };
                    section.Bookings.Add(booking);
                    _context.SaveChanges();

                    return Ok("Booking successful");
                }
                else
                {
                    return Conflict("Section is fully booked");
                }
            }
            else
            {
                return NotFound();
            }
        }




    }
}