using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationFront.Models.Requests
{
    public class AddSectionRequest
    {
        [Required]
        public TimeOnly duration { get; set; }
        [Required]
        public DateTime? time { get; set; }
        [Required]
        public string sectionType { get; set; }
        [Required]
        public int capacity { get; set; }
        public Instructor instructor { get; set; } = new();
    }

    public class Instructor
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string description { get; set; }
    }
}
