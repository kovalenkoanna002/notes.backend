using Microsoft.Extensions.DependencyInjection;
using Notes.Backend.Application.Services;
using Notes.Backend.Application.Validtions;
using Notes.Backend.Domain.Interfaces.Services;
using Notes.Backend.Domain.Interfaces.Validtions;
using Users.Backend.Application.Services;

namespace Notes.Backend.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.ServicesInit();
            services.ValidatorsInit();
        }

        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void ValidatorsInit(this IServiceCollection services)
        {
            services.AddScoped<INoteValidator, NoteValidator>();
        }
    }
}
