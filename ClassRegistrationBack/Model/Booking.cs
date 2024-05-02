namespace ClassRegistrationBack.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public Section Section { get; set; }
        public User User { get; set; }
    }
}
