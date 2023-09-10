using api.Dtos;

namespace api.Repositories.Services.Interfaces
{
    public interface IContactService
    {
        Task Add(CreateContactDto model);
        Task Remove(int Id);
        Task Update(int Id, UpdateContactDto model);
        Task<List<Contact>> GetByPerson(int personId);
    }
}