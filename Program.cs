using Data;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContactDBContext>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

app.MapGet("v1/contacts", async (IContactService service) =>
{
    var allContacts = await service.FindAllContactsAsync();

    return allContacts.Any() ? Results.Ok(allContacts) : Results.NotFound();
}).Produces<Contact>();

app.MapGet("v1/contacts/{id}", async (IContactService service, Guid id) =>
{
    var existingContact = await service.FindContactByIdAsync(id);

    return existingContact is not null ? Results.Ok(existingContact) : Results.NotFound();
}).Produces<Contact>();

app.MapPost("v1/contacts", async (IContactService service, Contact contact) =>
{
    var createdId = await service.CreateContactAsync(contact);
    return Results.Created($"/v1/contacts/{createdId}", createdId);
}).Produces<Contact>();

app.MapPut("v1/contacts", async (IContactService service, Contact contact) =>
{
    var existingContact = await service.FindContactByIdAsync(contact.Id);
    if (existingContact is null)
        return Results.NotFound();

    await service.UpdateContactAsync(contact.Id, existingContact);
    return Results.Ok("Contact updated successfully");
});

app.MapDelete("v1/contacts/{id}", async (IContactService service, Guid id) =>
{
    var existingContact = await service.FindContactByIdAsync(id);
    if (existingContact is null)
        return Results.NotFound();

    await service.DeleteContactAsync(id);
    return Results.NoContent();
});

app.Run();
