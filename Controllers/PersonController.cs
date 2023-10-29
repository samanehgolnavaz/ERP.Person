
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
        public async  Task<IActionResult> GettAllPeopleAsync(CancellationToken cancellationToken)
        {
            var persons =await _personRepository.GetAllPeopleAsync();
            return Ok(persons);
        }
        [HttpGet("{nationalId}")]
        public async Task<IActionResult> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken)
        {
            var person =await _personRepository.GetPersonByIdAsync(nationalId);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody]Models.Entities.Person person, CancellationToken cancellationToken)
        {
            await  _personRepository.AddPersonAsync(person);
            return CreatedAtAction(nameof(GetPersonByIdAsync), new { id = person.Id }, person);
        }

        [HttpPut("{nationaId}")]
        public async Task<IActionResult> UpdatePersonAsync(string nationalId, [FromBody]Models.Entities.Person person, CancellationToken cancellationToken)
        {
            if (nationalId != person.NationalId)
            {
                return BadRequest();
            }
            await _personRepository.UpdatePersonAsync(person);
            return NoContent();
        }

        [HttpDelete("{nationalId}")]
        public async Task<IActionResult> DeletePerson(string nationalId, CancellationToken cancellationToken)
        {
            var result=await _personRepository.DeletePersonAsync(nationalId);
            if (result)
            {
                return NoContent();

            }
            return NotFound();

        }

    }
}
