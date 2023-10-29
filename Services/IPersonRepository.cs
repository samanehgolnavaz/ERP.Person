namespace ERP.Person.Services
{
    public interface IPersonRepository
    {
        Models.Entities.Person GetPersonById(string  nationalId, CancellationToken cancellationToken=default);
        IEnumerable<Models.Entities.Person> GetAllPeople(CancellationToken cancellationToken=default);
        void AddPerson(Models.Entities.Person person, CancellationToken cancellationToken = default);
        void UpdatePerson(Models.Entities.Person person, CancellationToken cancellationToken=default);
        void DeletePerson(Guid id, CancellationToken cancellationToken = default);
    }
}
