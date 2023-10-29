using System.Diagnostics.Eventing.Reader;

namespace ERP.Person.Models.Entities
{
    public class Person
    {
        public Person()
        {
            Id = new Guid();
            Enable = true;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool   Gender { get; set; }
        public string NationalId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool Enable { get; set; }
    }
}
