using Notes.Backend.Domain.Dtos;
using Skreet2k.Common.Models;

namespace Notes.Backend.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultList<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<Result<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        Task<Result<UserDto>> GetUserByPasswordAndUserName(string name, string password, CancellationToken cancellationToken);

        Task<Result<UserDto>> CreateUserAsync(UserUpsertDto userDto, CancellationToken cancellationToken);

        Task<Result<UserDto>> UpdateUserAsync(int id, UserUpsertDto userDto, CancellationToken cancellationToken);

        Task<Result> RemoveUserAsync(int id, CancellationToken cancellationToken);
    }
}
