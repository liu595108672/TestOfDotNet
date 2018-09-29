using Microsoft.EntityFrameworkCore;

namespace TestForWebAPI.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options):base(options)
        {

        }

        public DbSet<PersonModel> Persons { set; get; }
    }
}