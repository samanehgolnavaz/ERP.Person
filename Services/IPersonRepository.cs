namespace ERP.Person.Services
{
    public interface IPersonRepository
    {
        ERP.Person.Models.Entities.Person GetPersonById(string  nationalId);
        IEnumerable<ERP.Person.Models.Entities.Person> GetAllPeople();
        void AddPerson(ERP.Person.Models.Entities.Person person);
        void UpdatePerson(ERP.Person.Models.Entities.Person person);
        void DeletePerson(Guid id);
    }
}
