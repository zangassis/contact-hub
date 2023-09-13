using Models;

namespace Services
{
    public interface IContactService
    {
        Task<List<Contact>> FindAllContactsAsync();
        Task<Contact> FindContactByIdAsync(Guid id);
        string GetPersonalContactFullName(Contact contact);
        Task<Guid> CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Guid id, Contact updatedContact);
        Task DeleteContactAsync(Guid id);
    }
}