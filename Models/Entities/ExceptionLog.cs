namespace ERP.Person.Models.Entities
{
    public class ExceptionLog
    {
        public ExceptionLog()
        {
            CreatedDate = DateTime.Now;
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
