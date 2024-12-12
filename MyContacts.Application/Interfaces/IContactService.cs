using MyContacts.Application.Models.DTOs;
using MyContacts.Application.Models.Requests;
using MyContacts.Domain.Entities;

namespace MyContacts.Application.Interfaces
{
    public interface IContactService
    {
        Task<Guid> CreateContactAsync(CreateContactReq req);
        ContactDto GetContactDto(Contact contact);
        Contact MapToContact(ContactDto contactDto);
        Task<IList<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdSpec(Guid id);
        Task<bool> IsExistContactByName(string name);
        Task UpdateContactAsync(Contact contact);
    }
}