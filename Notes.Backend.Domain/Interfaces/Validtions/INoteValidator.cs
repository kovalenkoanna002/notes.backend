using Notes.Backend.Domain.Dtos;
using Skreet2k.Common.Models;

namespace Notes.Backend.Domain.Interfaces.Validtions
{
    public interface INoteValidator
    {
        Task<Result> ValidateCreateNoteAsync(int userId, NoteUpsertDto noteDto, CancellationToken cancellationToken);
    }
}
