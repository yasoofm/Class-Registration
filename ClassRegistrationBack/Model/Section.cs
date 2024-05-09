namespace ClassRegistrationBack.Model
{
    public class Section
    {
        public int Id { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime Time { get; set; }
        public string SectionType { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }
        public Instructor Instructor { get; set; }
        public Gym Gym { get; set; }
    }

    public class SectionResponse
    {
        public int Id { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime Time { get; set; }
        public string SectionType { get; set; }
        public int Capacity { get; set; }
        public int Registered { get; set; }
        public InstructorResponse? Instructor { get; set; }
    }
}
