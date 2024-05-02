using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Model
{
    public class ClassContext : DbContext
    {
       public DbSet<Gym> gyms { get; set; }
       public DbSet<Address> addresses { get; set; }
       public DbSet<Booking> bookings { get; set; }
       public DbSet<Instructor> instructors { get; set; }
       public DbSet<Section> sections { get; set; }
        public DbSet<User> users { get; set; }

        public ClassContext(DbContextOptions<ClassContext> options) : base(options) { }

    }
}
