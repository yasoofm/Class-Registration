﻿namespace ClassRegistrationBack.Model
{
    public class EditProfile
    {

        public class EditProfileRequest
        { 
            public int? Id { get; set; }
            public string? Email { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? UserName { get; set; }
            public int? PhoneNumber { get; set; }
        }
    }
}
