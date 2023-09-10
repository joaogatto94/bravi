namespace api.Dtos
{
    public class CreateContactDto
    {
        public required int PersonId { get; set; }
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public string? Whatsapp { get; set; }
        public string? Email { get; set; }
    }
}