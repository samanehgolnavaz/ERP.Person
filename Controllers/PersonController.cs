﻿
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
        public async Task<IActionResult> CreatePersonAsync([FromBody]CreatePersonModel person, CancellationToken cancellationToken)
        {
            var createdPerson= await  _personRepository.AddPersonAsync(person);
            return CreatedAtAction(nameof(GetPersonByIdAsync), new { id = createdPerson.Id }, person);
        }

        [HttpPut("{nationalId}")]
        public async Task<IActionResult> UpdatePersonAsync(string nationalId, [FromBody]UpdatePersonModel person, CancellationToken cancellationToken)
        {
            var findNationalId= await _personRepository.FindPersonAsync(nationalId);
            if (!findNationalId)
            {
                return BadRequest();
            }
            var reult = await _personRepository.UpdatePersonAsync(nationalId, person);
            if (reult == null)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{nationalId}")]
        public async Task<IActionResult> ChangePersonStatusAsync(string nationalId, [FromBody] PersonStatusModel person, CancellationToken cancellationToken)
        {
            var findNationalId = await _personRepository.FindPersonAsync(nationalId);
            if (!findNationalId)
            {
                return BadRequest();
            }
            var reult = await _personRepository.ChangePersonStatusAsync(nationalId, person);
            if (reult == null)
                return NotFound();
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
