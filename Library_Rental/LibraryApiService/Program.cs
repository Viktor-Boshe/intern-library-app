using LibraryApiService.Interface;
using LibraryApiService.Repositories;
using LibraryApiService.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository>(provided => new UserRepository(connstring));
builder.Services.AddScoped<ILibraryRepository>(provided => new LibraryRepository(connstring));
builder.Services.AddScoped<ICheckoutRepository>(provided => new CheckoutRepository(connstring));

builder.Services.AddSingleton<TokenGenerator>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    return new TokenGenerator(secretKey);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters validationParameters) =>
            {
                return expires != null && expires > DateTime.UtcNow;
            }
        };
    });

var app = builder.Build();

app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
    DefaultFilesOptions = { DefaultFileNames = new[] { "FrontPage.html" } }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();