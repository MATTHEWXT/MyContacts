using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Application.Interfaces;
using MyContacts.Application.Models.DTOs;
using MyContacts.Application.Models.Requests;
using MyContacts.Domain.Entities;
using MyContacts.Infrastructure.Data;
using System.Reflection.Metadata.Ecma335;

namespace MyContacts.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly AppDbContext _dbContext;
        private readonly IPhoneNumberService _phoneNumberService;

        public ContactController(IContactService contactService, IPhoneNumberService phoneNumberService, AppDbContext dbContext)
        {
            _contactService = contactService;
            _dbContext = dbContext;
            _phoneNumberService = phoneNumberService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateContact([FromBody] CreateContactReq req)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (await _contactService.IsExistContactByName(req.Name))
                {
                    return StatusCode(500, new { message = "Name of contact already exist." });
                }

                var contactId = await _contactService.CreateContactAsync(req);

                List<(PhoneNumberDto, bool)> existingPhones = new List<(PhoneNumberDto, bool)>();

                foreach (var pn in req.PhoneNumbers)
                {
                    var isExist = await _phoneNumberService.IsExistPhoneNumber(pn.Number);
                    existingPhones.Add((pn, isExist));
                }

                req.PhoneNumbers.RemoveAll(pn => existingPhones.Any(ep => ep.Item1 == pn && ep.Item2));
             
                var contactPhones = req.PhoneNumbers
                    .Select(phone => new PhoneNumber(contactId, phone.Number, phone.Type))
                    .ToList();

                if (contactPhones.Count < 1)
                    throw new ArgumentNullException(nameof(contactPhones), "contactPhones cannot be null.");

                await _phoneNumberService.AddContactNumbers(contactPhones);
                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();

                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetContacts")]
        public async Task<ActionResult<IList<ContactDto>>> GetAllContacts()
        {
            try
            {
                var contactList = await _contactService.GetAllContactsAsync();

                var contactDtoList = contactList
                    .Select(c => _contactService.GetContactDto(c))
                    .ToList();

                return Ok(contactDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("updateContact")]
        public async Task<ActionResult> UpdateContact([FromBody] ContactDto contactDto)
        {
            try
            {
                var contactEntity = await _contactService.GetContactByIdSpec(contactDto.Id);

                if (contactEntity.Name != contactDto.Name && await _contactService.IsExistContactByName(contactDto.Name))
                {
                    return StatusCode(500, new { message = "Name of contact already exist." });
                }

                // Existence Validation of new phone numbers from request
                List<(PhoneNumberDto, bool)> existingPhones = new List<(PhoneNumberDto, bool)>();

                foreach (var pn in contactDto.PhoneNumbers)
                {
                    if (pn.Id != Guid.Empty)
                    {
                        var isExist = await _phoneNumberService.IsExistPhoneNumber(pn.Number);
                        existingPhones.Add((pn, isExist));
                    }
                    else
                    {
                        existingPhones.Add((pn, false));
                    }
                }
                // Remove phone numbers from contact if do not exist in request
                foreach (var existingPhoneNumber in contactEntity.PhoneNumbers.ToList())
                {
                    var existsInNewContact = contactDto.PhoneNumbers.Any(pn => pn.Number == existingPhoneNumber.Number);

                    if (!existsInNewContact)
                    {
                        contactEntity.PhoneNumbers.Remove(existingPhoneNumber);
                    }
                }

                contactDto.PhoneNumbers.RemoveAll(pn => existingPhones.Any(ep => ep.Item1 == pn && ep.Item2));
                // ---------------------------

                var newContact = _contactService.MapToContact(contactDto);

                contactEntity.Name = newContact.Name;
                contactEntity.Email = newContact.Email;
                contactEntity.Addres = newContact.Addres;
                contactEntity.Description = newContact.Description;
                contactEntity.ModifiedDate = DateTime.UtcNow;

                foreach (var newPhone in newContact.PhoneNumbers)
                {
                    var isExistingPhone = contactEntity.PhoneNumbers.FirstOrDefault(pn => pn.Id == newPhone.Id);

                    if (isExistingPhone == null)
                    {
                        newPhone.ContactId = contactDto.Id;
                        contactEntity.PhoneNumbers.Add(newPhone);
                    }
                    else
                    {
                        isExistingPhone.Number = newPhone.Number;
                        isExistingPhone.Type = newPhone.Type;
                        isExistingPhone.ModifiedDate = DateTime.UtcNow;
                    }
                }

                await _contactService.UpdateContactAsync(contactEntity);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
