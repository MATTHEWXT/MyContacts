using MyContacts.Application.Models.DTOs;
using MyContacts.Application.Models.Requests;
using MyContacts.Domain.Entities;
using MyContacts.UI.Components.Pages;

namespace MyContacts.UI.Services
{
    public class ContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactReq req)
        {
            var response = await _httpClient.PostAsJsonAsync("/contact/create", req);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to create contact: {errorContent}");
            }
        }

        public async Task<IList<ContactDto>> GetAllContactsAsync()
        {
            var contactList = await _httpClient.GetFromJsonAsync<IList<ContactDto>>("/contact/GetContacts");

            return contactList!;
        }

        public async Task UpdateContactAsync(ContactDto contactDto)
        {
            await _httpClient.PostAsJsonAsync("/contact/updateContact", contactDto);
        }
    }
}
