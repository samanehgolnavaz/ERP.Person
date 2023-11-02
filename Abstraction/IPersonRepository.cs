namespace ERP.Person.Abstraction
{
    public interface IPersonRepository
    {
        Task<Models.Entities.Person?> GetPersonByIdAsync(string nationalId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Models.Entities.Person>> GetAllPeopleAsync(CancellationToken cancellationToken = default);
        Task<Models.Entities.Person> AddPersonAsync(CreatePersonModel createPerson, CancellationToken cancellationToken = default);
        Task<Models.Entities.Person> UpdatePersonAsync(string nationalId,UpdatePersonModel person, CancellationToken cancellationToken = default);
        Task<Models.Entities.Person?> ChangePersonStatusAsync(string nationalId, PersonStatusModel person, CancellationToken cancellationToken = default);
        Task<bool> DeletePersonAsync(string nationalId, CancellationToken cancellationToken = default);
        Task<bool> FindPersonAsync(string nationalId, CancellationToken cancellationToken = default);

    }
}
