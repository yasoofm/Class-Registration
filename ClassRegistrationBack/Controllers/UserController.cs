using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClassRegistrationBack.Model.EditProfile;

namespace ClassRegistrationBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class UserController : ControllerBase

    {
        private readonly ILogger<UserController> _logger;
        private readonly ClassContext _context;

        public UserController(ILogger<UserController> logger, ClassContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult InstructorDetails(int Id)
        {
        using (var context = _context)
            {

                var instructorsDetails = context.Instructors.Where(r => r.Id == Id);
                if (instructorsDetails == null)
                {
                    return NotFound();
                }
                return Ok(instructorsDetails);
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GymResponse>> Gyms()
        {
            return _context.Gyms.Select(p => new GymResponse() { Id = p.Id, Name = p.Name }).ToList();
            
        }
        
        [HttpGet("sections/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<List<SectionResponse>> GetSections(int id)
        {
            try
            {
                return _context.Sections.Where(x => x.Gym.Id == id).Include(p => p.Bookings).Include(p => p.Instructor).Select(p => new SectionResponse
                {
                    Id = p.Id,
                    Time = p.Time,
                    Duration = p.Duration,
                    SectionType = p.SectionType,
                    Capacity = p.Capacity,
                    Registered = p.Bookings.Count,
                    Instructor = new InstructorResponse
                    {
                        Id = p.Instructor.Id,
                        FirstName = p.Instructor.FirstName,
                        LastName = p.Instructor.LastName,
                        Description = p.Instructor.Description,
                        PhoneNumber = p.Instructor.PhoneNumber,
                    }
                })
                .ToList();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserAccount> GetProfile(int id)
        {
            return _context.Users.Find(id);
        }

        [HttpPatch("[action]")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditProfile(int id, EditProfileRequest request)
        {
            var profile = _context.Users.Find(id);
            if(request.FirstName != null)
            {
                profile.FirstName = request.FirstName;
            }

            if (request.LastName != null)
            {
                profile.LastName = request.LastName;
            }

            if (request.UserName != null)
            {
                profile.UserName = request.UserName;
            }

            if (request.PhoneNumber != null)
            {
                profile.PhoneNumber = (int)request.PhoneNumber;
            }

            if (request.Email != null)
            {
                profile.Email = request.Email;
            }

            if(request.Password != null)
            {
                profile.Password = request.Password;
            }

            _context.SaveChanges();

            return Created();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Book(int userId, int sectionId)
        {
            var user = _context.Users.Find(userId);
            var section = _context.Sections
                .Include(s => s.Bookings)
                .FirstOrDefault(s => s.Id == sectionId);

            if (user != null && section != null)
            {
                if (section.Bookings.Count < section.Capacity) 
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

        [HttpGet("Bookings/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<BookingResponse> GetBookings(int id)
        {
            return _context.Bookings.Where(x => x.User.Id == id).Include(x => x.Section).Select(x => new BookingResponse
            {
                CreateAt = x.CreateAt,
                Id = x.Id,
                Section = new SectionResponse
                {
                    Id = x.Section.Id,
                    Duration = x.Section.Duration,
                    Time = x.Section.Time,
                    SectionType = x.Section.SectionType,
                    Capacity = x.Section.Capacity,
                }
            });
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AdressDetails(int Id)
        {

            using (var context = _context)
            {

                var instructorsDetails = context.Addresses.Where(a => a.Id == Id);
                if (instructorsDetails == null)
                {
                    return NotFound();
                }
                return Ok(instructorsDetails);
            }
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null) { 
            
            return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

         return NoContent();

        }
    }
    }


