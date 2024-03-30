using Notes.Backend.Domain.Entities;

namespace Notes.Backend.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User?> GetUserByPasswordAndUserName(string password, string name, CancellationToken cancellationToken);
        Task<User?> GetUserByUserName(string name, CancellationToken cancellationToken);
        Task<User> CreateAsync(User user, CancellationToken cancellationToken);
        Task RemoveAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
        IQueryable<User> GetAllUsers();
    }
}
