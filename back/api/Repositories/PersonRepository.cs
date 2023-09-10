using api.Repositories.Interfaces;

namespace api.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApiDbContext context) : base(context)
        {
        }
    }
}