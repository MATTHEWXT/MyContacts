using MyContacts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Application.Models.DTOs
{
    public class PhoneNumberDto
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Number { get; set; } = string.Empty;
        public PhoneType Type { get; set; }
    }
}
