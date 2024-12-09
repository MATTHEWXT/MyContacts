using MyContacts.Domain.Core.Models;

namespace MyContacts.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
        public string? Description {  get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public Category Category { get; set; } = null!;

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
