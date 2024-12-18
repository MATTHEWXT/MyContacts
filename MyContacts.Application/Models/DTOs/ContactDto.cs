﻿using MyContacts.Domain.Entities;
using System;
using System.Collections.Generic;
namespace MyContacts.Application.Models.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Addres { get; set; }
        public string? Description { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();
    }
}
