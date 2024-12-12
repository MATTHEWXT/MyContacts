using MyContacts.Application.Models.DTOs;
using MyContacts.Domain.Entities;

namespace MyContacts.Application.Interfaces
{
    public interface IPhoneNumberService
    {
        Task AddContactNumbers(IList<PhoneNumber> contactPhones);
        Task<bool> IsExistPhoneNumber(string phoneNumber);
    }
}