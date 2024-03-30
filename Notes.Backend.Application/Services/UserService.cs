using Mapster;
using Microsoft.EntityFrameworkCore;
using Notes.Backend.Application.Resources;
using Notes.Backend.Domain.Dtos;
using Notes.Backend.Domain.Entities;
using Notes.Backend.Domain.Interfaces.Repositories;
using Notes.Backend.Domain.Interfaces.Services;
using Skreet2k.Common.Models;

namespace Users.Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultList<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsers()
                .Select(user => user.Adapt<UserDto>())
                .ToListAsync(cancellationToken);

            if (!users.Any())
            {
                return new ResultList<UserDto>
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
                    ReturnCode = (int)ErrorCode.UserNotFound21
                };
            }

            return new ResultList<UserDto>
            {
                Content = users,
            };
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(id, cancellationToken);

            if (user == null)
            {
                return new Result<UserDto>
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
                    ReturnCode = (int)ErrorCode.UserNotFound21
                };
            }

            return new Result<UserDto>
            {
                Content = user.Adapt<UserDto>(),
            };
        }

        public async Task<Result<UserDto>> GetUserByPasswordAndUserName(string name, string password, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByPasswordAndUserName(password, name, cancellationToken);

            if (user == null)
            {
                return new Result<UserDto>
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
                    ReturnCode = (int)ErrorCode.UserNotFound21
                };
            }

            return new Result<UserDto>
            {
                Content = user.Adapt<UserDto>(),
            };
        }

        public async Task<Result<UserDto>> CreateUserAsync(UserUpsertDto userDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByUserName(userDto.Name, cancellationToken);

            if (user != null)
            {
                return new Result<UserDto>
                {
                    ErrorMessage = ErrorMessages.UserCreationFailed,
                    ReturnCode = (int)ErrorCode.NoteCreationFailed14
                };
            }

            user = new User
            {
                Name = userDto.Name,
                Password = userDto.Password,
            };

            user = await _repository.CreateAsync(user, cancellationToken);

            return new Result<UserDto>
            {
                Content = user.Adapt<UserDto>(),
            };
        }

        public async Task<Result<UserDto>> UpdateUserAsync(int id, UserUpsertDto userDto, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByIdAsync(id, cancellationToken);

            if (existingUser == null)
            {
                return new Result<UserDto>
                {
                    ErrorMessage = ErrorMessages.UserUpdateFailed,
                    ReturnCode = (int)ErrorCode.UserUpdateFailed22
                };
            }

            existingUser.Name = userDto.Name;
            existingUser.Password = userDto.Password;

            var user = await _repository.UpdateAsync(existingUser, cancellationToken);

            return new Result<UserDto>
            {
                Content = user.Adapt<UserDto>(),
            };
        }

        public async Task<Result> RemoveUserAsync(int id, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByIdAsync(id, cancellationToken);

            if (existingUser == null)
            {
                return new Result
                {
                    ErrorMessage = ErrorMessages.NoteDeletionFailed,
                    ReturnCode = (int)ErrorCode.UserDeletionFailed23
                };
            }

            await _repository.RemoveAsync(existingUser, cancellationToken);

            return new Result();
        }
    }
}
