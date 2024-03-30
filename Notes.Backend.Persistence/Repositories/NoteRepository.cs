using Microsoft.EntityFrameworkCore;
using Notes.Backend.Domain.Entities;
using Notes.Backend.Domain.Interfaces.Repositories;

namespace Notes.Backend.Persistence.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesContext _context;

        public NoteRepository(NotesContext context)
        {
            _context = context;
        }

        public async Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<Note> CreateAsync(Note note, CancellationToken cancellationToken)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync(cancellationToken);

            return note;
        }

        public async Task RemoveAsync(Note note, CancellationToken cancellationToken)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Note> UpdateAsync(Note note, CancellationToken cancellationToken)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync(cancellationToken);

            return note;
        }

        public IQueryable<Note> GetAllNotes()
        {
            return _context.Notes.AsNoTracking();
        }
    }
}
