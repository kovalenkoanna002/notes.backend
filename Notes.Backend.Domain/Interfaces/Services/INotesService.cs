using Notes.Backend.Domain.Dtos;
using Skreet2k.Common.Models;

namespace Notes.Backend.Domain.Interfaces.Services
{
    public interface INotesService
    {
        Task<ResultList<NoteDto>> GetAllNotesAsync(int userId, CancellationToken cancellationToken);
        Task<Result<NoteDto>> GetNoteByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<NoteDto>> CreateNoteAsync(int userId, NoteUpsertDto noteDto, CancellationToken cancellationToken);
        Task<Result<NoteDto>> UpdateNoteAsync(int id, NoteUpsertDto noteDto, CancellationToken cancellationToken);
        Task<Result> RemoveNoteAsync(int id, CancellationToken cancellationToken);
    }
}
