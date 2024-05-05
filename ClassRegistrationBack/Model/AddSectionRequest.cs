namespace ClassRegistrationBack.Model
{
    public class AddSectionRequest
    {
        public TimeOnly Duration { get; set; }
        public DateTime Time { get; set; }
        public string SectionType { get; set; }
        public int Capacity { get; set; }
        public InstructorResponse? Instructor { get; set; }
    }
}
