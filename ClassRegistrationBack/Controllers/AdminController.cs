using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Controllers
{

    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly ClassContext _context;

        public AdminController(ILogger<AdminController> logger, ClassContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }


        [HttpPost("add-gym")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddGym(AddGymRequset addGymRequest)
        {
            try
            {
                var gym = new Gym()
                {
                    Address = addGymRequest.Address,
                    Name = addGymRequest.Name
                };
                _context.gyms.Add(gym);
                _context.SaveChanges();
                return Created(nameof(AddGym) ,new { Id = gym.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("get-gyms")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<GymResponse>> GetGyms()
        {
            try
            {
                return _context.gyms.Include(p => p.Sections).Include(p => p.Address).Select(p => new GymResponse 
                {
                    Id = p.Id, Sections = p.Sections.Select(x => new SectionResponse
                    { 
                        Id = x.Id, Capacity = x.Capacity,
                        Duration = x.Duration, SectionType = x.SectionType, Time = x.Time,
                        Instructor = new InstructorResponse 
                        {
                            Description = x.Instructor.Description, FirstName = x.Instructor.FirstName, LastName = x.Instructor.LastName,
                            Id = x.Instructor.Id, PhoneNumber = x.Instructor.PhoneNumber
                        },
                    }).ToList(),
                    Address = p.Address, Name = p.Name 
                })
                .ToList();

            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet("get-sections")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<SectionResponse>> GetSections(int id)
        {
            try
            {
                return _context.sections.Where(x => x.Id == id).Select(p => new SectionResponse
                {
                    Id = p.Id,
                    Time = p.Time,
                    Duration = p.Duration,
                    SectionType = p.SectionType,
                    Capacity = p.Capacity

                })
                .ToList();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet("get-instructor")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<InstructorResponse>> GetInstructors()
        {
            try
            {
                return _context.instructors.Include(x => x.Sections).Select(p => new InstructorResponse
                {
                    Id = p.Id,
                    PhoneNumber = p.PhoneNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Description = p.Description,
                    Sections = p.Sections.Select(p => new SectionResponse
                    {
                        Id = p.Id,
                        Capacity = p.Capacity,
                        SectionType = p.SectionType,
                        Time = p.Time,
                        Duration = p.Duration,
                    }).ToList()
                    

                })
                .ToList();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpGet("get-users")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserResponse>> GetUsers()
        {
            try
            {
                return _context.users.Include(p => p.Bookings).ThenInclude(p => p.Section).Select(p => new UserResponse
                {
                    Id = p.Id,
                    PhoneNumber = p.PhoneNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    UserName = p.UserName,
                    Bookings = p.Bookings.Select(p => new BookingResponse
                    {
                        Id = p.Id,
                        CreateAt = p.CreateAt,
                        Section = new SectionResponse 
                        { Id = p.Section.Id,
                            Capacity = p.Section.Capacity,
                            Duration = p.Section.Duration, 
                            SectionType = p.Section.SectionType,
                            Time = p.Section.Time },

                    }).ToList()
                })
                .ToList();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteGym(int id)
        {
            try
            {
                var gym = _context.gyms.Find(id);
                if (gym == null)
                {
                    return NotFound();
                }

                _context.gyms.Remove(gym);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var userdelete = _context.users.Find(id);
                if (userdelete == null)
                {
                    return NotFound();
                }

                _context.users.Remove(userdelete);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSection(int id)
        {
            try
            {
                var deletesection = _context.sections.Find(id);
                if (deletesection == null)
                {
                    return NotFound();
                }

                _context.sections.Remove(deletesection);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteInstructor(int id)
        {
            try
            {
                var deleteinstructor = _context.instructors.Find(id);
                if (deleteinstructor == null)
                {
                    return NotFound();
                }

                _context.instructors.Remove(deleteinstructor);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

    //   editgym - editinstructor - editsection