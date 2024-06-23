using LibraryApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
var app = builder.Build();

app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
    DefaultFilesOptions = { DefaultFileNames = new[] { "FrontPage.html" } }
});

app.UseAuthorization();

app.MapControllers();

app.Run();