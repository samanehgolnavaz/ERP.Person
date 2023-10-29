using Azure;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ERP.Person.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _dbContext;
        public PersonRepository(PersonDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<Models.Entities.Person> AddPersonAsync(Models.Entities.Person person,CancellationToken cancellationToken = default)
        {
            var addPerson = (await _dbContext.People.AddAsync(person, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return addPerson;
        }

        public async Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var id =await _dbContext.People.Where(n => n.NationalId==nationalId).Select(n =>n.Id).FirstOrDefaultAsync();
            var person =await _dbContext.People.FindAsync(id);
            if (person != null)
            {
                person.Enable = false;
                //_dbContext.People.Remove(person);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Models.Entities.Person>> GetAllPeopleAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.People.Where(p => p.Enable==true).ToListAsync();
        }

        public async Task<Models.Entities.Person?> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var findPerson= await _dbContext.People.FirstOrDefaultAsync(p => p.NationalId == nationalId && p.Enable == true);
            return findPerson;
        }

        public async Task<Models.Entities.Person?> UpdatePersonAsync(Models.Entities.Person person, CancellationToken cancellationToken = default)
        {

           var updatePerson=( _dbContext.People.Update(person)).Entity;
           await _dbContext.SaveChangesAsync(cancellationToken);

            return updatePerson;
        }
    }
}
