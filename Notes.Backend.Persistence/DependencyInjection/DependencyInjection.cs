using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Backend.Domain.Interfaces.Repositories;
using Notes.Backend.Persistence.Repositories;

namespace Notes.Backend.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<NotesContext>(options => { options.UseNpgsql(connectionString); });

            services.RepositoriesInit();
        }

        public static void RepositoriesInit(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
