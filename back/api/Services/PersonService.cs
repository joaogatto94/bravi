using api.Dtos;
using api.Repositories.Interfaces;
using api.Repositories.Services.Interfaces;

namespace api.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        
        public async Task Add(CreatePersonDto model)
        {
            await personRepository.Add(new Person {Name = model.Name});
            await personRepository.SaveChanges();
        }

        public async Task Remove(int Id)
        {
            var person = await personRepository.GetById(Id);
            if (person != null)
            {
                personRepository.Remove(person);
                await personRepository.SaveChanges();
            }
        }

        public async Task Update(int Id, UpdatePersonDto model)
        {
            var person = await personRepository.GetById(Id);
            if (person != null)
            {
                person.Name = model.Name;
                personRepository.Update(person);
                await personRepository.SaveChanges();
            }
        }
        
        public async Task<List<Person>> GetAll()
        {
            return await personRepository.GetAll();
        }
    }
}