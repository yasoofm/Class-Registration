namespace ClassRegistrationBack.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public Section Section { get; set; }
        public UserAccount User { get; set; }
    }

    public class BookingResponse
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public SectionResponse Section { get; set; }
    }
}
