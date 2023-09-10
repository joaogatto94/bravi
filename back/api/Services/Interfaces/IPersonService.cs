using api.Dtos;

namespace api.Repositories.Services.Interfaces
{
    public interface IPersonService
    {
        Task Add(CreatePersonDto model);
        Task Remove(int Id);
        Task Update(int Id, UpdatePersonDto model);
        Task<List<Person>> GetAll();
    }
}