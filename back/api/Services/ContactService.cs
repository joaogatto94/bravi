using api.Dtos;
using api.Repositories.Interfaces;
using api.Repositories.Services.Interfaces;

namespace api.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        public ContactService(IContactRepository ContactRepository)
        {
            contactRepository = ContactRepository;
        }
        
        public async Task Add(CreateContactDto model)
        {
            await contactRepository.Add(new Contact
            {
                PersonId = model.PersonId,
                Phone = model.Phone,
                Whatsapp = model.Whatsapp,
                Name = model.Name,
                Email = model.Email
            });

            await contactRepository.SaveChanges();
        }

        public async Task Remove(int Id)
        {
            var contact = await contactRepository.GetById(Id);
            if (contact != null)
            {
                contactRepository.Remove(contact);
                await contactRepository.SaveChanges();
            }
        }

        public async Task Update(int Id, UpdateContactDto model)
        {
            var contact = await contactRepository.GetById(Id);
            if (contact != null)
            {
                contact.Name = model.Name;
                contact.Phone = model.Phone;
                contact.Whatsapp = model.Whatsapp;
                contact.Email = model.Email;
                contactRepository.Update(contact);
                await contactRepository.SaveChanges();
            }
        }
        
        public async Task<List<Contact>> GetByPerson(int personId)
        {
            return await contactRepository.Find(c => c.PersonId == personId);
        }
    }
}