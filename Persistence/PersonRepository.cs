

namespace ERP.Person.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _dbContext;
        public PersonRepository(PersonDbContext dbContext)

        {
            _dbContext = dbContext;
        }

        public async Task<Models.Entities.Person> AddPersonAsync(Models.Entities.Person person, CancellationToken cancellationToken = default)
        {
            var addPerson = (await _dbContext.People.AddAsync(person, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return addPerson;
        }

        public async Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var id = await _dbContext.People.Where(n => n.NationalId == nationalId).Select(n => n.Id).FirstOrDefaultAsync();
            var person = await _dbContext.People.FindAsync(id);
            if (person != null)
            {
                person.Enable = false;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> FindPersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var id = await  _dbContext.People.Where(i => i.NationalId == nationalId).Select(i => i.NationalId).FirstOrDefaultAsync();
            if (id is null)
                return false;
            return true;
        }

        public async Task<IEnumerable<Models.Entities.Person>> GetAllPeopleAsync(CancellationToken cancellationToken = default)
        {
            var people= await _dbContext.People.Where(p => p.Enable).OrderByDescending(n => n.Family).Page(pageIndex: 2, pageSize: 10).ToListAsync();
            return people;
        }

        public async Task<Models.Entities.Person> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var findPerson = await _dbContext.People.FirstOrDefaultAsync(p => p.NationalId == nationalId && p.Enable);
            return findPerson;
        }

        public async Task<Models.Entities.Person?> UpdatePersonAsync(string nationalId,Models.Entities.Person person, CancellationToken cancellationToken = default)
        {
            var id = await _dbContext.People.Where(i => i.NationalId==nationalId).Select(i => i.Id).FirstOrDefaultAsync();
            var findPerson = await _dbContext.People.FindAsync(id);
            if (findPerson is null)
            {
                return null;
            }
            findPerson.Name=person.Name;
            findPerson.Family=person.Family;
            findPerson.Gender = person.Gender;
            findPerson.Country = person.Country;
            findPerson.City=person.City;
            findPerson.Address=person.Address;
            findPerson.Email=person.Email;
            findPerson.Mobile=person.Mobile;
            var updatePerson = _dbContext.People.Update(findPerson).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return updatePerson;
        }
       
    }
}
