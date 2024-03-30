FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR src
COPY Notes.Backend.Api/Notes.Backend.Api.csproj Notes.Backend.Api/Notes.Backend.Api.csproj
COPY Notes.Backend.Application/Notes.Backend.Application.csproj Notes.Backend.Application/Notes.Backend.Application.csproj
COPY Notes.Backend.Persistence/Notes.Backend.Persistence.csproj Notes.Backend.Persistence/Notes.Backend.Persistence.csproj
COPY Notes.Backend.Domain/Notes.Backend.Domain.csproj Notes.Backend.Domain/Notes.Backend.Domain.csproj
RUN dotnet restore "Notes.Backend.Api/Notes.Backend.Api.csproj"
COPY . .
RUN dotnet build "Notes.Backend.Api/Notes.Backend.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Notes.Backend.Api/Notes.Backend.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app/
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Notes.Backend.Api.dll"]