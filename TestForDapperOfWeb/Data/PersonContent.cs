using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestForDapperOfWeb.Models;

namespace TestForDapperOfWeb.Data
{
    public class PersonContent:DbContext
    {
        IConfiguration Configuration { set; get; }
        public PersonContent()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public PersonContent(DbContextOptions<PersonContent> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetValue<string>("DBConnect:ConnectionString"));
            }
        }

        public DbSet<Person> Person { set; get; }

        public Person GetPersonByName(string Name)
        {
            Person personToReturn = new Person();


            return personToReturn;
        }
    }
}
