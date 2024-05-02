using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
