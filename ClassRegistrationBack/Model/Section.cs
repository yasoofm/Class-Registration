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

}
