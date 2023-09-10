using api.Repositories.Interfaces;

namespace api.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApiDbContext context) : base(context)
        {
        }
    }
}