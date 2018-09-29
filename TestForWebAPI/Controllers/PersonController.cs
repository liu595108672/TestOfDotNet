using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestForWebAPI.Models;
using System.Linq;

namespace TestForWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly PersonContext _personContext;
        public PersonController(PersonContext personContext)
        {
            _personContext = personContext;
            if (_personContext.Persons.Count()==0)
            {
                _personContext.Add(new PersonModel { FirstName = "Zhaoyang", LastName = "Liu", Age = 24, Gender = (short)1 });
                _personContext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<PersonModel> GetAllPersons()
        {
            return _personContext.Persons.ToList();
        }

        [HttpGet( "byFirstName",Name = "ByFirstName")]
        public IActionResult GetByFirstName(string firstName)
        {
            var tmpPerson = _personContext.Persons.FirstOrDefault(x => x.FirstName == firstName);
            if (tmpPerson == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(tmpPerson);
            }
        }

        [HttpPost("Create")]
        public bool CreatePerson(string firstName, string lastName, int age, short gender)
        {
            _personContext.Add(new PersonModel { FirstName = firstName, LastName = lastName, Age = age, Gender = gender });
            _personContext.SaveChanges();
            return true;
        }
      
        [HttpPost("CreateByBody")]
        public bool CreatePersonByBody([FromBody] PersonModel person)
        {
            if (person == null)
            {
                return false;
            }

            _personContext.Persons.Add(person);
            _personContext.SaveChanges();

            return true;
        }
    }
}
