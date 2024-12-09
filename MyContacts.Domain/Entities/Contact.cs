using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Enums;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyContacts.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Guid Id { get; }
        public Guid CategoryId { get; }
        public string Name { get; }
        public string? Email { get; }
        public string? Addres { get; }
        public string? Description {  get; }
        public List<PhoneNumber> PhoneNumbers { get; }

        public Contact(Guid categoryId, string name, string? email, string? addres, string? description)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = name;
            Email = email;
            Addres = addres;
            Description = description;
            PhoneNumbers = new List<PhoneNumber>();
        }
         
        public void Validate()
        {
            if (CategoryId == Guid.Empty)
                throw new ArgumentException("CategoryId is required.");

            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name of contact is required.");
        }
    }
}
