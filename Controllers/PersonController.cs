
using ERP.Person.Services;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERP.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult GettAllPeople(CancellationToken cancellationToken)
        {
            var persons = _personRepository.GetAllPeople();
            return Ok(persons);
        }
        [HttpGet("{nationalId}")]
        public IActionResult GetPersonById(string nationalId, CancellationToken cancellationToken)
        {
            var person = _personRepository.GetPersonById(nationalId);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpPost]
        public IActionResult CreatePerson([FromBody]Models.Entities.Person person, CancellationToken cancellationToken)
        {
            _personRepository.AddPerson(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody]Models.Entities.Person person, CancellationToken cancellationToken)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }
            _personRepository.UpdatePerson(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(Guid id, CancellationToken cancellationToken)
        {
            _personRepository.DeletePerson(id);
            return NoContent();
        }

    }
}
