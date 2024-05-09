using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationFront.Models.Requests
{
    public class AddGymRequest
    {
        [Required]
        public string name {  get; set; }
        [Required]
        public Address address { get; set; } = new();
    }

    public class Address
    {
        [Required]
        public string buildingNumber { get; set; }
        public string avenue { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public string area { get; set; }
        [Required]
        public int block { get; set; }
    }
}
