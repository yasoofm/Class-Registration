using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
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
                    Address = new Address 
                    { 
                        Area = addGymRequest.Address.Area, Avenue = addGymRequest.Address.Avenue,
                        Block = addGymRequest.Address.Block, BuildingNumber = addGymRequest.Address.BuildingNumber,
                        Street = addGymRequest.Address.Street
                    },
                    Name = addGymRequest.Name
                };
                _context.Gyms.Add(gym);
                _context.SaveChanges();
                return Created(nameof(AddGym) ,new { Id = gym.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }




        [HttpPost("add-section/{id}")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSection(AddSectionRequest addSectionRequest, int id)
        {
            try
            {
                var gym = _context.Gyms.Find(id);
                if (gym == null)
                {
                    return NotFound();
                }

                var section = new Section()
                {
                    Instructor = new Instructor
                    {
                        PhoneNumber = addSectionRequest.Instructor.PhoneNumber,
                        Description = addSectionRequest.Instructor.Description,
                        FirstName = addSectionRequest.Instructor.FirstName,
                        LastName = addSectionRequest.Instructor.LastName
                    },
                    Duration = addSectionRequest.Duration,
                    Time = addSectionRequest.Time,
                    SectionType = addSectionRequest.SectionType,
                    Capacity = addSectionRequest.Capacity,
                    Gym = gym
                };
                _context.Sections.Add(section);
                _context.SaveChanges();
                return Created(nameof(AddSection), new { Id = section.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpPost("add-intructor")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddIntructor(AddInstructorRequest addInstructorRequest)
        {
            try
            {
                var intructor = new Instructor()
                {
                PhoneNumber = addInstructorRequest.PhoneNumber,
                Description = addInstructorRequest.Description,
                FirstName = addInstructorRequest.FirstName,
                LastName =addInstructorRequest.LastName

                };
                _context.Instructors.Add(intructor);
                _context.SaveChanges();
                return Created(nameof(AddGym), new { Id = intructor.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet("get-gyms")]
        [ProducesResponseType(typeof(List<GymResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<GymResponse>> GetGyms()
        {
            return _context.Gyms.Select(p =>  new GymResponse() { Id = p.Id, Name = p.Name }).ToList();               
        }


        [HttpGet("get-sections/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<SectionResponse>> GetSections(int id)
        {
            try
            {
                return _context.Sections.Where(x => x.Gym.Id == id).Include(p => p.Instructor).Select(p => new SectionResponse
                {
                    Id = p.Id,
                    Time = p.Time,
                    Duration = p.Duration,
                    SectionType = p.SectionType,
                    Capacity = p.Capacity,
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


        [HttpGet("get-instructors")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<InstructorResponse>> GetInstructors()
        {
            try
            {
                return _context.Instructors.Include(x => x.Sections).Select(p => new InstructorResponse
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
                return _context.Users.Include(p => p.Bookings).ThenInclude(p => p.Section).Select(p => new UserResponse
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

        [HttpDelete("delete-gym/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteGym(int id)
        {
            try
            {
                var gym = _context.Gyms.Find(id);
                if (gym == null)
                {
                    return NotFound();
                }

                _context.Gyms.Remove(gym);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("delete-user/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var userdelete = _context.Users.Find(id);
                if (userdelete == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(userdelete);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete-section/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSection(int id)
        {
            try
            {
                var deletesection = _context.Sections.Find(id);
                if (deletesection == null)
                {
                    return NotFound();
                }

                _context.Sections.Remove(deletesection);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        
        
        [HttpDelete("delete-instructor/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteInstructor(int id)
        {
            try
            {
                var deleteinstructor = _context.Instructors.Find(id);
                if (deleteinstructor == null)
                {
                    return NotFound();
                }

                _context.Instructors.Remove(deleteinstructor);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpPatch("edit-gym/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditGym(int id, AddGymRequset request)
        {

            try
            {
                var gym = _context.Gyms.Find(id);
                if (gym == null)
                {
                    return NotFound();
                }
                gym.Name = request.Name;
                gym.Address = new Address()
                {
                    BuildingNumber = request.Address.BuildingNumber,
                    Avenue = request.Address.Avenue,
                    Street = request.Address.Street,
                    Area = request.Address.Area,
                    Block = request.Address.Block
                };
                _context.SaveChanges();

                return Created(nameof(EditGym), new { id = gym.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }



        [HttpPatch("edit-instructor/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditInstructor(int id, AddInstructorRequest request)
        {

            try
            {
                var instructor = _context.Instructors.Find(id);
                if (instructor == null)
                {
                    return NotFound();
                }
                instructor.PhoneNumber = request.PhoneNumber;
                instructor.FirstName = request.FirstName;
                instructor.LastName = request.LastName;
                _context.SaveChanges();

                return Created(nameof(EditInstructor), new { id = instructor.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPatch("edit-section/{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditSection(int id, AddSectionRequest request)
        {

            try
            {
                var section = _context.Sections.Find(id);
                if (section == null)
                {
                    return NotFound();
                }
                section.Capacity = request.Capacity;
                section.Instructor = new Instructor()
                {
                    FirstName = request.Instructor.FirstName,
                    LastName = request.Instructor.LastName,
                    Description = request.Instructor.Description,
                    PhoneNumber = request.Instructor.PhoneNumber
                };
                section.Duration = request.Duration;
                _context.SaveChanges();

                return Created(nameof(EditSection), new { id = section.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
