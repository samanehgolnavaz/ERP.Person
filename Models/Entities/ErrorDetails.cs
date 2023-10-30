using System.Text.Json;

namespace ERP.Person.Models.Entities
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? RequestId { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);

        }
    }
}
