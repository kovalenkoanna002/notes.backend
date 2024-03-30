namespace Notes.Backend.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Password { get; set; } = default!;

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
