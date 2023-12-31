﻿

using ERP.Person.Services.Interfaces;

namespace ERP.Person.Models.Entities
{
    public class Person
    {
        public readonly IGuidGenerator guidGenerator;
        public Person()
        {
            Id = Guid.NewGuid();
            Enable = true;
            IsDeleted = false;
        }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        public bool   Gender { get; set; }
        [Required]
        [MaxLength(10)]
        public string NationalId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool Enable { get; set; }
        public bool IsDeleted { get; set; }
    }
}
