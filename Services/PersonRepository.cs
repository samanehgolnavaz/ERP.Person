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

        public void AddPerson(Models.Entities.Person person,CancellationToken cancellationToken = default)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        public void DeletePerson(Guid id, CancellationToken cancellationToken = default)
        {
            var person = _dbContext.People.Find(id);
            if (person != null)
            {
                _dbContext.People.Remove(person);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Models.Entities.Person> GetAllPeople(CancellationToken cancellationToken = default)
        {
            return _dbContext.People.ToList();
        }

        public Models.Entities.Person GetPersonById(string nationalId, CancellationToken cancellationToken = default)
        {
            return _dbContext.People.FirstOrDefault(p => p.NationalId == nationalId);
        }

        public void UpdatePerson(Models.Entities.Person person, CancellationToken cancellationToken = default)
        {
            _dbContext.People.Update(person);
            _dbContext.SaveChanges();
        }
    }
}
