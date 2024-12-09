using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Enums;
namespace MyContacts.Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public Contact Contact { get; set; } = null!;

        public PhoneNumber(Guid contactId, string number, PhoneType type) {
            Id = Guid.NewGuid();
            ContactId = contactId;
            Number = number;
            Type = type;
        }

        public void Validate()
        {
            if (ContactId == Guid.Empty)
                throw new ArgumentException("ContactId is required.");

            if (string.IsNullOrWhiteSpace(Number))
                throw new ArgumentException("Phone number is required.");

            if (!Enum.IsDefined(typeof(PhoneType), Type))
                throw new ArgumentException($"Invalid phone type: {Type}");
        }
    }
}
