using Microsoft.EntityFrameworkCore;
using LabApp.Models;

namespace LabApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Diagnostician> Diagnosticians { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
