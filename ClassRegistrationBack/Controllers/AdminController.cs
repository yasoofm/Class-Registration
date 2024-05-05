using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Mvc;

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
                _context.Gyms.Add(gym);
                _context.SaveChanges();
                return Created(nameof(AddGym) ,new { Id = gym.Id });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
