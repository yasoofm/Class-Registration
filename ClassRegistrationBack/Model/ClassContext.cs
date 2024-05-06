using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Model
{
    public class ClassContext : DbContext
    {
       public DbSet<Gym> Gyms { get; set; }
       public DbSet<Address> Addresses { get; set; }
       public DbSet<Booking> Bookings { get; set; }
       public DbSet<Instructor> Instructors { get; set; }
       public DbSet<Section> Sections { get; set; }
        public DbSet<UserAccount> Users { get; set; }

        public ClassContext(DbContextOptions<ClassContext> options) : base(options) { }

    }
}
