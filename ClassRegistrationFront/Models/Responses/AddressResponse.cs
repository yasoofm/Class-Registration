namespace ClassRegistrationFront.Models.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string BuildingNumber { get; set; }
        public string? Avenue {  get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public int Block {  get; set; }
    }
}
