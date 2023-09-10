using api.Repositories.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace api.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly IPersonService personService;
        private readonly ApiDbContext context;


        public PersonServiceTests(IPersonService personService, ApiDbContext context)
        {
            this.personService = personService;
            this.context = context;
        }

        [Fact]
        public async Task TestAdd()
        {
            var personName = "test add person";
            await personService.Add(new Dtos.CreatePersonDto
            {
                Name = personName
            });

            var users = await context.Persons.ToListAsync();

            Assert.Contains(users, x => x.Name == personName);
        }
        
        [Fact]
        public async Task TestRemove()
        {
            var id = 100;
            var name = "test remove person";
            await AddPersonToDb(context, id, name);
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            Assert.NotNull(person);

            await personService.Remove(id);
            person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            Assert.Null(person);
        }
        
        [Fact]
        public async Task TestUpdate()
        {
            var id = 101;
            var name = "test update person";
            await AddPersonToDb(context, id, name);
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            Assert.NotNull(person);

            var nameAfterUpdate = "test update person with updated name";
            await personService.Update(id, new Dtos.UpdatePersonDto
            {
                Name = nameAfterUpdate
            });
            person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            Assert.NotNull(person);
            Assert.Equal(nameAfterUpdate, person.Name);
        }
        
        [Fact]
        public async Task TestGetAll()
        {
            var length = 20;
            for (int i = length; i <= length; i++)
            {
                var name = $"person test get all person {i}";
                await personService.Add(new Dtos.CreatePersonDto
                {
                    Name = name
                });
            }
            var dbUsers = await context.Persons.ToListAsync();
            var users = await personService.GetAll();
            
            Assert.Equal(dbUsers.Count, users.Count);
            foreach (var user in dbUsers)
            {
                Assert.Contains(users, x => x.Name == user.Name);
            }
        }

        public static async Task AddPersonToDb(ApiDbContext dbContext, int? id, string name)
        {
            var person = new Person
            {
                Name = name
            };

            if (id != null)
            {
                person.Id = (int)id;
            }
            await dbContext.Persons.AddAsync(person);
            await dbContext.SaveChangesAsync();
        }
    }
}