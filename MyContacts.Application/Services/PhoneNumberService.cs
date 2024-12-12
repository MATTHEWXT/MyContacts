using MyContacts.Application.Interfaces;
using MyContacts.Application.Models.DTOs;
using MyContacts.Domain.Core.Repositories;
using MyContacts.Domain.Core.Specifications;
using MyContacts.Domain.Entities;
using System.Numerics;

namespace MyContacts.Application.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IBaseRepositoryProvider _provider;

        public PhoneNumberService(IBaseRepositoryProvider provider)
        {
            _provider = provider;
        }

        public async Task AddContactNumbers(IList<PhoneNumber> contactPhones)
        {
            await _provider.GetRepository<PhoneNumber>().AddListAsync(contactPhones);
        }

        public async Task<bool> IsExistPhoneNumber(string phoneNumber)
        {
            return await _provider.GetRepository<PhoneNumber>().IsExistEntity(PhoneNumberSpecification.GetPhoneNumberById(phoneNumber));
        }
    }
}
