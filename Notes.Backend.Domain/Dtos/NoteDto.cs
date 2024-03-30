namespace Notes.Backend.Domain.Dtos
{
    public record NoteDto(int Id, string Title, string Content, DateTimeOffset ModifiedTime);
}
