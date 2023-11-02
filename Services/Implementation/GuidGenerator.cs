using ERP.Person.Services.Interfaces;

namespace ERP.Person.Services.Implementation
{
    public class GuidGenerator : IGuidGenerator
    {
        private Guid _guid = Guid.NewGuid();

        public Guid  GetUniqueGuid()
        {
            return _guid;
        }
    }
}
