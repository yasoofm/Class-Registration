﻿namespace ClassRegistrationBack.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string BuildingNumber { get; set; }
        public string? Avenue { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public int Block { get; set; }
    }

    public class AddressRequest
    {
        public string BuildingNumber { get; set; }
        public string? Avenue { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public int Block { get; set; }

    }
}
