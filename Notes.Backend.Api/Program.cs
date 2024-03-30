using Notes.Backend.Api.Middlewares;
using Notes.Backend.Api.Providers.AuthHandlers;
using Notes.Backend.Api.Providers.AuthHandlers.Constants;
using Notes.Backend.Api.Providers.AuthHandlers.Scheme;
using Notes.Backend.Application.DependencyInjection;
using Notes.Backend.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
    options => options.DefaultScheme = AuthSchemeConstants.BasicAuthScheme)
    .AddScheme<BasicAuthSchemeOptions, BasicAuthHandler>(
        AuthSchemeConstants.BasicAuthScheme, options => { });

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapControllers();

app.Run();