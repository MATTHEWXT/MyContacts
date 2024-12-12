using AutoMapper;
using MyContacts.Application.Interfaces;
using MyContacts.Application.Models.DTOs;
using MyContacts.Application.Models.Requests;
using MyContacts.Domain.Core.Repositories;
using MyContacts.Domain.Core.Specifications;
using MyContacts.Domain.Entities;

namespace MyContacts.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepositoryProvider _provider;

        public ContactService(IMapper mapper, IBaseRepositoryProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider), "BaseRepositoryProvider cannot be null.");

            _mapper = mapper;
            _provider = provider;
        }

        public ContactDto GetContactDto(Contact contact)
        {
            return _mapper.Map<ContactDto>(contact);
        }

        public Contact MapToContact(ContactDto contactDto)
        {
            return _mapper.Map<Contact>(contactDto);
        }

        public async Task<Guid> CreateContactAsync(CreateContactReq req)
        {
            var contact = new Contact(
                req.Name,
                req.Email,
                req.Addres,
                req.Description
                );
            contact.Validate();

            await _provider.GetRepository<Contact>().AddAsync(contact);

            return contact.Id;
        }

        public async Task<IList<Contact>> GetAllContactsAsync()
        {
            return await _provider.GetRepository<Contact>().ListAsync(ContactSpecification.GetAllContacts());
        }

        public async Task<Contact> GetContactByIdSpec(Guid id)
        {
            return await _provider.GetRepository<Contact>().GetEntityAsync(ContactSpecification.GetContactById(id));
        }
        public async Task<bool> IsExistContactByName(string name)
        {
            return await _provider.GetRepository<Contact>().IsExistEntity(ContactSpecification.GetContactByName(name));
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _provider.GetRepository<Contact>().UpdateAsync(contact);
        }
    }
}
