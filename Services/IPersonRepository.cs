namespace ERP.Person.Services
{
    public interface IPersonRepository
    {
        Task<Models.Entities.Person>  GetPersonByIdAsync(string  nationalId, CancellationToken cancellationToken=default);
        Task<IEnumerable<Models.Entities.Person>> GetAllPeopleAsync(CancellationToken cancellationToken=default);
        Task<Models.Entities.Person> AddPersonAsync(Models.Entities.Person person, CancellationToken cancellationToken = default);
        Task<Models.Entities.Person> UpdatePersonAsync(Models.Entities.Person person, CancellationToken cancellationToken=default);
        Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default);
    }
}
