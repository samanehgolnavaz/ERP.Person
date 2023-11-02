using ERP.Person.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERP.Person.Services.Implementation
{
    public class PersonService :IPersonService 
    {
        private readonly IPersonRepository _personRepository;

        public async Task<Models.Entities.Person> AddPersonAsync(CreatePersonModel createPerson, CancellationToken cancellationToken = default)
        {
            return await _personRepository.AddPersonAsync(createPerson, cancellationToken);
        }

        public async Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            return await _personRepository.DeletePersonAsync(nationalId, cancellationToken);
        }

        public async Task<Models.Entities.Person?> ChangePersonStatusAsync(string nationalId, PersonStatusModel person, CancellationToken cancellationToken = default)
        {
           return await _personRepository.ChangePersonStatusAsync(nationalId, person, cancellationToken);
        }

        public async Task<bool> FindPersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            return await _personRepository.FindPersonAsync(nationalId, cancellationToken);
        }

        public async Task<IEnumerable<Models.Entities.Person>> GetAllPeopleAsync(CancellationToken cancellationToken = default)
        {
            return await _personRepository.GetAllPeopleAsync(cancellationToken);
        }

        public async Task<Models.Entities.Person?> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            return await _personRepository.GetPersonByIdAsync(nationalId, cancellationToken);   
        }

        public async Task<Models.Entities.Person> UpdatePersonAsync(string nationalId, UpdatePersonModel person, CancellationToken cancellationToken = default)
        {
            return await _personRepository.UpdatePersonAsync(nationalId, person, cancellationToken);
        }

    }
}
