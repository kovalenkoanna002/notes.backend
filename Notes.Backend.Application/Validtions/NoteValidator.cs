using Notes.Backend.Application.Resources;
using Notes.Backend.Domain.Dtos;
using Notes.Backend.Domain.Interfaces.Repositories;
using Notes.Backend.Domain.Interfaces.Validtions;
using Skreet2k.Common.Models;

namespace Notes.Backend.Application.Validtions
{
    public class NoteValidator : INoteValidator
    {
        public readonly IUserRepository _repository;

        public NoteValidator(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> ValidateCreateNoteAsync(int userId, NoteUpsertDto noteDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                return new Result
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
                    ReturnCode = (int)ErrorCode.UserNotFound21
                };
            }

            if (string.IsNullOrEmpty(noteDto.Title) ||
                string.IsNullOrEmpty(noteDto.Content))
            {
                return new Result
                {
                    ErrorMessage = ErrorMessages.NoteCreationFailed,
                    ReturnCode = (int)ErrorCode.NoteCreationFailed14
                };
            }

            return new Result();
        }
    }
}
