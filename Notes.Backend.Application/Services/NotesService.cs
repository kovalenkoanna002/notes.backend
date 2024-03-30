using Mapster;
using Microsoft.EntityFrameworkCore;
using Notes.Backend.Application.Resources;
using Notes.Backend.Domain.Dtos;
using Notes.Backend.Domain.Entities;
using Notes.Backend.Domain.Interfaces.Repositories;
using Notes.Backend.Domain.Interfaces.Services;
using Notes.Backend.Domain.Interfaces.Validtions;
using Skreet2k.Common.Models;

namespace Notes.Backend.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INoteRepository _repository;

        private readonly INoteValidator _validator;
        public NotesService(INoteRepository repository, INoteValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ResultList<NoteDto>> GetAllNotesAsync(int userId, CancellationToken cancellationToken)
        {
            var notes = await _repository.GetAllNotes()
                .Where(note => note.UserId == userId)
                .Select(note => note.Adapt<NoteDto>())
                .ToListAsync(cancellationToken);

            if (!notes.Any())
            {
                return new ResultList<NoteDto>
                {
                    ErrorMessage = ErrorMessages.NoteNotFound,
                    ReturnCode = (int)ErrorCode.NoteNotFound11
                };
            }

            return new ResultList<NoteDto>
            {
                Content = notes,
            };
        }

        public async Task<Result<NoteDto>> GetNoteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var note = await _repository.GetByIdAsync(id, cancellationToken);

            if (note == null)
            {
                return new Result<NoteDto>
                {
                    ErrorMessage = ErrorMessages.NoteNotFound,
                    ReturnCode = (int)ErrorCode.NoteNotFound11
                };
            }

            return new Result<NoteDto>
            {
                Content = note.Adapt<NoteDto>(),
            };
        }

        public async Task<Result<NoteDto>> CreateNoteAsync(int userId, NoteUpsertDto noteDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateCreateNoteAsync(userId, noteDto, cancellationToken);

            if (!validationResult.IsSuccess)
            {
                return new Result<NoteDto>
                {
                    ErrorMessage = validationResult.ErrorMessage,
                    ReturnCode = validationResult.ReturnCode
                };
            }

            var note = new Note
            {
                UserId = userId,
                Title = noteDto.Title,
                Content = noteDto.Content,
                ModifiedTime = DateTimeOffset.UtcNow
            };

            note = await _repository.CreateAsync(note, cancellationToken);

            return new Result<NoteDto>
            {
                Content = note.Adapt<NoteDto>(),
            };
        }

        public async Task<Result<NoteDto>> UpdateNoteAsync(int id, NoteUpsertDto noteDto, CancellationToken cancellationToken)
        {
            var existingNote = await _repository.GetByIdAsync(id, cancellationToken);

            if (existingNote == null)
            {
                return new Result<NoteDto>
                {
                    ErrorMessage = ErrorMessages.NoteUpdateFailed,
                    ReturnCode = (int)ErrorCode.NoteUpdateFailed12
                };
            }

            existingNote.Title = noteDto.Title ?? existingNote.Title;
            existingNote.Content = noteDto.Content ?? existingNote.Content;
            existingNote.ModifiedTime = DateTimeOffset.UtcNow;

            var note = await _repository.UpdateAsync(existingNote, cancellationToken);

            return new Result<NoteDto>
            {
                Content = note.Adapt<NoteDto>(),
            };
        }

        public async Task<Result> RemoveNoteAsync(int id, CancellationToken cancellationToken)
        {
            var existingNote = await _repository.GetByIdAsync(id, cancellationToken);

            if (existingNote == null)
            {
                return new Result
                {
                    ErrorMessage = ErrorMessages.NoteDeletionFailed,
                    ReturnCode = (int)ErrorCode.NoteDeletionFailed13
                };
            }

            await _repository.RemoveAsync(existingNote, cancellationToken);

            return new Result();
        }
    }
}
