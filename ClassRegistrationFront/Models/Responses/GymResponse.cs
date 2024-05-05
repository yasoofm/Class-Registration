using ClassRegistrationFront.Models.Responses;

namespace ClassRegistrationFront.Models.Entities
{
    public class GymResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SectionResponse> sections { get; set; }
        public AddressResponse Address { get; set; }
    }
}
