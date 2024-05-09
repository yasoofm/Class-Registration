namespace ClassRegistrationFront.Models.Responses
{
    public class SectionResponse
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime Time { get; set; }
        public string SectionType { get; set; }
        public int Registered {  get; set; }
        public InstructorResponse Instructor { get; set; }
    }
}
