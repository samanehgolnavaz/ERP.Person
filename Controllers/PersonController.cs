
using ERP.Person.Services.Implementation;
using ERP.Person.Services.Interfaces;

namespace ERP.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly CodeValidation  _codeValidation;
        public PersonController(IPersonService personService,CodeValidation codeValidation)
        {
            _personService = personService;
            _codeValidation = codeValidation;
        }

        [HttpGet]
        public async  Task<IActionResult> GettAllPeopleAsync(CancellationToken cancellationToken)
        {
            var persons =await _personService.GetAllPeopleAsync();
            return Ok(persons);
        }
        [HttpGet("{nationalId}")]
        public async Task<IActionResult> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken)
        {
            if (!CodeValidation.IsValidNationaCode(nationalId))
            {
                return NotFound();
            }
            var person =await _personService.GetPersonByIdAsync(nationalId);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody]CreatePersonModel person, CancellationToken cancellationToken)
        {
            var createdPerson= await _personService.AddPersonAsync(person);
            return CreatedAtAction(nameof(GetPersonByIdAsync), new { id = createdPerson.Id }, person);
        }

        [HttpPut("{nationalId}")]
        public async Task<IActionResult> UpdatePersonAsync(string nationalId, [FromBody]UpdatePersonModel person, CancellationToken cancellationToken)
        {
            if (!CodeValidation.IsValidNationaCode(nationalId))
            {
                return NotFound();
            }
            var findNationalId= await _personService.FindPersonAsync(nationalId);
            if (!findNationalId)
            {
                return BadRequest();
            }
            var reult = await _personService.UpdatePersonAsync(nationalId, person);
            if (reult == null)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{nationalId}")]
        public async Task<IActionResult> ChangePersonStatusAsync(string nationalId, [FromBody] PersonStatusModel person, CancellationToken cancellationToken)
        {
            if (!CodeValidation.IsValidNationaCode(nationalId))
            {
                return NotFound();
            }
            var findNationalId = await _personService.FindPersonAsync(nationalId);
            if (!findNationalId)
            {
                return BadRequest();
            }
            var reult = await _personService.ChangePersonStatusAsync(nationalId, person);
            if (reult == null)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{nationalId}")]
        public async Task<IActionResult> DeletePerson(string nationalId, CancellationToken cancellationToken)
        {
            if (!CodeValidation.IsValidNationaCode(nationalId))
            {
                return NotFound();
            }
            var result=await _personService.DeletePersonAsync(nationalId);
            if (result)
            {
                return NoContent();

            }
            return NotFound();

        }

    }
}
