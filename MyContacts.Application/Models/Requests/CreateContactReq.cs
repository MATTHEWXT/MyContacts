using MyContacts.Application.Models.DTOs;
using MyContacts.Domain.Entities;
using MyContacts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Application.Models.Requests
{
    public class CreateContactReq
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Addres { get; set; }
        public string? Description { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();
    }
}
