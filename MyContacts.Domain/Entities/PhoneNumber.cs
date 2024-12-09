using MyContacts.Domain.Core.Models;
using MyContacts.Domain.Enums;
using System.Xml.Linq;
namespace MyContacts.Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public Guid Id { get; }
        public Guid ContactId { get; }
        public string Number { get; }
        public PhoneType Type { get; }

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
