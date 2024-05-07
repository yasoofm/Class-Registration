using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ClassRegistrationFront.Models.Requests
{
    public class EditUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
