using Microsoft.EntityFrameworkCore;
using Notes.Backend.Domain.Entities;
using Notes.Backend.Domain.Interfaces.Repositories;

namespace Notes.Backend.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesContext _context;

        public UserRepository(NotesContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByPasswordAndUserName(string password, string name, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name.Equals(name) && u.Password.Equals(password), cancellationToken);
        }

        public async Task<User?> GetUserByUserName(string name, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name.Equals(name), cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task RemoveAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users.AsNoTracking();
        }
    }
}
