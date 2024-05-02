namespace ClassRegistrationBack.Model
{
    public class User
    {
       
            public int Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int PhoneNumber { get; set; }
            public bool IsAdmin { get; set; }
            public List<Booking> Bookings {get; set;}

            private User() { }
            public static User Create(string username, string password, bool isAdmin = false)
            {
                return new User
                {
                    UserName = username,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                    IsAdmin = isAdmin
                };
            }
            public bool VerifyPassword(string pwd) => BCrypt.Net.BCrypt.EnhancedVerify(pwd, this.Password);


        }
    
}
