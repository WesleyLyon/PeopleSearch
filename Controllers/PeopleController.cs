using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Models;

namespace PeopleSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleSearchContext _context;

        public PeopleController(PeopleSearchContext context)
        {
            _context = context;

            _context.Database.Migrate();

            _context.Database.ExecuteSqlCommand(@"
                if not exists (select 1 from sys.tables where name = 'Person')
                begin;
                    create table Person
                    (
	                    Id int identity primary key,
	                    FirstName nvarchar(100),
	                    LastName nvarchar(100),
	                    Address nvarchar(max),
	                    Age int,
	                    Interests nvarchar(max),
	                    Image nvarchar(255)
                    );
                end;

                if ((select count(1) from Person) < 1)
                begin;
                    insert into Person (FirstName, LastName, Address, Age, Interests, Image)
                    values
                        ('Wesley', 'Lyon', '2214 Rendezvous Rd. Idaho Falls, Id. 83402', 29, 'Camping, Hiking, Cars', 'wesley.jpg'),
                        ('Ashlee', 'Lyon', '2214 Rendezvous Rd. Idaho Falls, Id. 83402', 37, 'Camping, Interior Design', 'ashlee.jpg'),
                        ('Leon', 'Peterson', '783 Wooten Way Blackfoot, Id 83221', 64, 'Spending time with grandkids', 'leon.jpg'),
                        ('Tiffany', 'Woolstenhulme', '2911 Shadow Mountain Trail Idaho Falls, Id. 83402', 34, 'Camping, Scrapbooking', 'tiffany.jpg');
                end;");
        }

        //GET: api/People
        [HttpGet("[action]")]
        public IEnumerable<Person> GetPersons()
        {
            IEnumerable<Person> persons = GetAllPeople();

            return persons;
        }

        //POST: api/People
        [HttpPost("[action]")]
        public IEnumerable<Person> GetPersons([FromBody] ApiPostData body)
        {
            string lowerSearch = body.SearchString.ToLower();

            IEnumerable<Person> persons = GetAllPeople();

            //If there is search string then filter the results based on it.
            if (lowerSearch.Length > 0)
            {
                persons = persons.Where(p => p.FirstName.ToLower().Contains(lowerSearch) || p.LastName.ToLower().Contains(lowerSearch));
            }

            return persons;
        }

        #region future items
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
        #endregion

        #region private methods
        private IEnumerable<Person> GetAllPeople ()
        {
            System.Threading.Thread.Sleep(3000);

            return _context.Person.Select(p => new Person
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                Age = p.Age,
                Interests = p.Interests,
                Image = p.Image,
            });
        }

        #endregion
    }
}