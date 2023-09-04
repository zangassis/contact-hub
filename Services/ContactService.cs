using Data;
using Models;

namespace Services;

public class ContactService
{
    private readonly ContactRepository _repository;

    public ContactService(ContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Contact>> GetAllContactsAsync()
    {
        return await _repository.FindAllContactsAsync();
    }

    public async Task<Guid> CreateContactAsync(Contact contact)
    {


        return await _repository.InsertAsync(contact);
    }

    public async Task UpdateContactAsync(Guid id, Contact updatedContact)
    {


        var existingContact = await _repository.FindContactByIdAsync(id);
        if (existingContact != null)
        {
            await _repository.UpdateAsync(updatedContact, existingContact);
        }
    }

    public async Task DeleteContactAsync(Guid id)
    {


        var existingContact = await _repository.FindContactByIdAsync(id);
        if (existingContact != null)
        {
            await _repository.DeleteAsync(existingContact);
        }
    }
}
