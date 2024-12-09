namespace MyContacts.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Contacts = new List<Contact>();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name of contact is required.");
        }
    }
}
