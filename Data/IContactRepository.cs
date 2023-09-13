using Models;

namespace Data
{
    public interface IContactRepository
    {
        Task<List<Contact>> FindAllContactsAsync();
        Task<Contact> FindContactByIdAsync(Guid id);
        Task<Guid> InsertAsync(Contact contact);
        Task UpdateAsync(Contact contact, Contact existingContact);
        Task DeleteAsync(Contact contact);
    }
}