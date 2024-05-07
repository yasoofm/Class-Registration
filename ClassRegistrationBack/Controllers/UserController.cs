﻿using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClassRegistrationBack.Model.EditProfile;

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
        [HttpGet("[action]")]
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
        public ActionResult<IEnumerable<Gym>> Gyms()
        {
            var gyms = _context.Gyms;

            if (gyms == null)
            {
                return NotFound();
            }
            return Ok(gyms);
        }

        [HttpGet("/sections")]
        public IEnumerable<Section> GetAll()
        {
            return _context.Sections;
        }

       

        [HttpGet("[action]/{id}")]
        public ActionResult<UserAccount> GetProfile(int id)
        {
            return _context.Users.Find(id);
        }

        [HttpPatch("[action]")]
        public IActionResult EditProfile(int id, EditProfileResponse request)
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


            _context.SaveChanges();

            return Created();
        }

        [HttpPost("[action]")]
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
        public IEnumerable<Booking> GetBooking(int id)
        {
            return _context.Bookings.Where(x => x.User.Id == id);
        }

        [HttpGet("[action]")]
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


