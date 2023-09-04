namespace Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class ContactRepository
{
    private readonly ContactDBContext _db;

    public ContactRepository(ContactDBContext db)
    {
        _db = db;
    }

    public async Task<List<Contact>> FindAllContactsAsync()
    {
        return await _db.Contacts.ToListAsync();
    }

    public async Task<Contact> FindContactByIdAsync(Guid id)
    {
        var contact = await _db.Contacts.SingleOrDefaultAsync(c => c.Id == id);
        return contact;
    }

    public async Task<Guid> InsertAsync(Contact contact)
    {
        contact.Id = Guid.NewGuid();
        contact.CreatedOn = DateTime.Now;

        await _db.AddAsync(contact);
        await _db.SaveChangesAsync();
        return contact.Id;
    }

    public async Task UpdateAsync(Contact contact, Contact existingContact)
    {
        existingContact.FullName = contact.FullName;
        existingContact.PhoneNumber = contact.PhoneNumber;
        existingContact.EmailAddress = contact.EmailAddress;
        existingContact.Address = contact.Address;
        existingContact.IsDeleted = contact.IsDeleted;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        _db.Remove(contact);
        await _db.SaveChangesAsync();
    }
}