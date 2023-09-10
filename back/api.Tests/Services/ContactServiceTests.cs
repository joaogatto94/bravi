using api.Repositories.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly IContactService contactService;
        private readonly ApiDbContext context;


        public ContactServiceTests(IContactService contactService, ApiDbContext context)
        {
            this.contactService = contactService;
            this.context = context;
        }

        [Fact]
        public async Task TestAdd()
        {
            var personId = await GetPersonId();
            var contact = new Dtos.CreateContactDto
            {
                Name = "test add contact",
                PersonId = personId,
                Email = "email@email.com",
                Phone = "999999999",
                Whatsapp= "99999999"
            };
            
            await contactService.Add(contact);

            var person = await context.Persons.Include(x => x.Contacts).FirstAsync(x => x.Id == personId);
            Assert.Contains(person.Contacts, x => x.Name == contact.Name);
        }
        
        [Fact]
        public async Task TestRemove()
        {
            var personId = await GetPersonId();
            var contactId = 102;
            var contactCreate = new Dtos.CreateContactDto
            {
                Name = "test update contact",
                PersonId = personId,
                Email = "testupdatecontact@email.com",
                Phone = "99999999",
                Whatsapp = "99999999"
            };
            await AddContactToDb(context, contactId, contactCreate);
            
            await contactService.Remove(contactId);
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
            Assert.Null(contact);
        }
        
        [Fact]
        public async Task TestUpdate()
        {
            var personId = await GetPersonId();
            var contactId = 103;
            var contactCreate = new Dtos.CreateContactDto
            {
                Name = "test update contact",
                PersonId = personId,
                Email = "testupdatecontact@email.com",
                Phone = "99999999",
                Whatsapp = "99999999"
            };
            await AddContactToDb(context, contactId, contactCreate);

            var contactUpdate = new Dtos.UpdateContactDto
            {
                Name = "test update contact",
                Email = "testupdatecontact@updated.com",
                Phone = "888888888",
                Whatsapp = "888888888"                
            };
            await contactService.Update(contactId, contactUpdate);
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
            Assert.NotNull(contact);
            Assert.Equal(contact.Name, contactUpdate.Name);
            Assert.Equal(contact.Email, contactUpdate.Email);
            Assert.Equal(contact.Whatsapp, contactUpdate.Whatsapp);
            Assert.Equal(contact.Phone, contactUpdate.Phone);
        }
        
        [Fact]
        public async Task TestGetByPerson()
        {
            var personId = await GetPersonId();
            var length = 20;

            for (int i = 1; i <= length; i++)
            {
                await AddContactToDb(context, null, new Dtos.CreateContactDto
                {
                    Name = "test update contact",
                    PersonId = personId,
                    Email = "testupdatecontact@email.com",
                    Phone = "99999999",
                    Whatsapp = "99999999"
                });
            }
            
            var dbContacts = await context.Contacts.Where(c => c.PersonId == personId).ToListAsync();
            var contacts = await contactService.GetByPerson(personId);

            Assert.Equal(dbContacts.Count, contacts.Count);

            foreach (var contact in dbContacts)
            {
                Assert.Contains(contacts, c => c.Id == contact.Id);
            }
        }

        private async Task<int> GetPersonId()
        {
            var person = await context.Persons.FirstOrDefaultAsync();

            if (person == null) 
            {
                var personName = "test update contact";
                await PersonServiceTests.AddPersonToDb(context, null, personName);
                person = await context.Persons.FirstOrDefaultAsync();
            }

            return person.Id;
        }

        public static async Task AddContactToDb(ApiDbContext dbContext, int? contactId, Dtos.CreateContactDto model)
        {
            var contact = new Contact
            {
                PersonId = model.PersonId,
                Email = model.Email,
                Name = model.Email,
                Phone = model.Phone,
                Whatsapp = model.Whatsapp,
            };

            if (contactId != null)
            {
                contact.Id = (int)contactId;
            }

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
        }
    }
}