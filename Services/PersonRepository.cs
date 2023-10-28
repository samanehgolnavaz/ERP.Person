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

        public void AddPerson(Models.Entities.Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        public void DeletePerson(Guid id)
        {
            var person = _dbContext.People.Find(id);
            if (person != null)
            {
                _dbContext.People.Remove(person);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Models.Entities.Person> GetAllPeople()
        {
            return _dbContext.People.ToList();
        }

        public Models.Entities.Person GetPersonById(string nationalId)
        {
            return _dbContext.People.FirstOrDefault(p => p.NationalId == nationalId);
        }

        public void UpdatePerson(Models.Entities.Person person)
        {
            _dbContext.People.Update(person);
            _dbContext.SaveChanges();
        }
    }
}
