using MyContacts.Domain.Core.Models;

namespace MyContacts.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
        public string? Description { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public Contact(string name, string? email, string? addres, string? description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Addres = addres;
            Description = description;
            PhoneNumbers = new List<PhoneNumber>();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name of contact is required.");
        }
    }
}
