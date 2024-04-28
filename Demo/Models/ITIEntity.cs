using Microsoft.EntityFrameworkCore;
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo.Models
{
    public class ITIEntity : IdentityDbContext<ApplicationUser>
    {


        public ITIEntity():base()
        {
            
        }

        public ITIEntity(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=MonfiaDotNetQ3;Integrated Security=true; TrustServerCertificate = True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
