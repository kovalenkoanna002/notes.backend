namespace Notes.Backend.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTimeOffset ModifiedTime { get; set; }

        public User? User { get; set; }
    }
}
