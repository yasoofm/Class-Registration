namespace ClassRegistrationFront.Models.Responses
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public SectionResponse Section { get; set; }
    }
}
