using CitizenWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CitizenWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Citizen> Citizens { get; set; }
    }


}
