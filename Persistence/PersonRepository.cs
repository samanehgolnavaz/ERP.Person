

using ERP.Person.Services.Interfaces;

namespace ERP.Person.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _dbContext;
        private readonly IGuidGenerator _guidGenerator;
        public PersonRepository(PersonDbContext dbContext,IGuidGenerator guidGenerator)

        {
            _dbContext = dbContext;
            _guidGenerator = guidGenerator;

        }

        public async Task<Models.Entities.Person> AddPersonAsync(CreatePersonModel createPerson, CancellationToken cancellationToken = default)
        {
            var person=new Models.Entities.Person();
            person.Name = createPerson.Name;
            person.Family = createPerson.Family;
            person.Email = createPerson.Email;
            person.City = createPerson.City;
            person.Country= createPerson.Country;
            person.Mobile = createPerson.Mobile;
            person.Gender = createPerson.Gender;
            person.NationalId= createPerson.NationalId;
            person.Id=_guidGenerator.GetUniqueGuid();
            person.IsDeleted = false;
            person.Enable = true;
            var addPerson = (await _dbContext.People.AddAsync(person, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return addPerson;
        }

        public async Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var id = await _dbContext.People
                .Where(n => n.NationalId == nationalId)
                .Select(n => n.Id)
                .FirstOrDefaultAsync();
            if  (id != Guid.Empty)
            {
                var person = await _dbContext.People.FindAsync(id);
                if (person != null)
                {
                    person.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;         
        }

        public async Task<Models.Entities.Person?> ChangePersonStatusAsync(string nationalId, PersonStatusModel person, CancellationToken cancellationToken = default)
        {
            var id = await _dbContext.People.Where(i => i.NationalId == nationalId).Select(i => i.Id).FirstOrDefaultAsync();
            var findPerson = await _dbContext.People.FindAsync(id);
            if (findPerson is null)
            {
                return null;
            }
            findPerson.Enable = person.Enable;
            var updatePerson = _dbContext.People.Update(findPerson).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return updatePerson;
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
            var people= await _dbContext.People.Where(p => p.Enable && !p.IsDeleted).OrderByDescending(n => n.Family).Page(pageIndex: 2, pageSize: 10).ToListAsync();
            return people;
        }

        public async Task<Models.Entities.Person?> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            var findPerson = await _dbContext.People.FirstOrDefaultAsync(p => p.NationalId == nationalId && p.Enable && !p.IsDeleted);
            if  (findPerson is null) return null;
            return findPerson;
        }

        public async Task<Models.Entities.Person> UpdatePersonAsync(string nationalId, UpdatePersonModel person, CancellationToken cancellationToken = default)
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
