﻿using AutoMapper;
using MyContacts.Application.Models.DTOs;
using MyContacts.Domain.Entities;

namespace MyContacts.Application.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumberDto, PhoneNumber>()
                .ConstructUsing(src => new PhoneNumber(
                    src.ContactId,
                    src.Number,
                    src.Type
                ));
        }
    }
}
