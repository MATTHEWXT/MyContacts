using MyContacts.Domain.Entities;


namespace MyContacts.Domain.Core.Specifications
{
    public class ContactSpecification
    {
        public static BaseSpecification<Contact> GetAllContacts()
        {
            var specification = new BaseSpecification<Contact>(null!) { };
            specification.AddInclude(c => c.PhoneNumbers);

            return specification;
        }

        public static BaseSpecification<Contact> GetContactById(Guid id)
        {
            var specification = new BaseSpecification<Contact>(c => c.Id == id) { };
            specification.AddInclude(c => c.PhoneNumbers);

            return specification;
        }
        public static BaseSpecification<Contact> GetContactByName(string name)
        {
            return new BaseSpecification<Contact>(c => c.Name == name) { };
        }
    }
}
