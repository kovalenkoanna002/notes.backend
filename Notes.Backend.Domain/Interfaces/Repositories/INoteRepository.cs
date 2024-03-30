using Notes.Backend.Domain.Entities;

namespace Notes.Backend.Domain.Interfaces.Repositories
{
    public interface INoteRepository
    {
        Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Note> CreateAsync(Note note, CancellationToken cancellationToken);
        Task RemoveAsync(Note note, CancellationToken cancellationToken);
        Task<Note> UpdateAsync(Note note, CancellationToken cancellationToken);
        IQueryable<Note> GetAllNotes();
    }
}
