using Microsoft.EntityFrameworkCore;

namespace api
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        private string DbPath { get; }
        private bool OverrideOnConfiguring { get; }

        
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            DbPath = "";
            OverrideOnConfiguring = false;
        }
        public ApiDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "api.db");
            OverrideOnConfiguring = true;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (OverrideOnConfiguring)
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Contact> Contacts { get; } = new();
    }

    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Whatsapp { get; set; }
        public string? Email { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}