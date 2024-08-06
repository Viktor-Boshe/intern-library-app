using LibraryApiService.Interface;
using LibraryApiService.Middleware;
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

builder.Services.AddScoped<IEmailSender>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var email = configuration["EmailSettings:Email"];
    var password = configuration["EmailSettings:Password"];
    return new EmailSenderRepository(email, password, connstring);
});

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
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(token))
                {
                    var tokenGenerator = context.HttpContext.RequestServices.GetRequiredService<TokenGenerator>();
                    var principal = tokenGenerator.ValidateToken(token);
                    if (principal != null)
                    {
                        context.HttpContext.User = principal;
                    }
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
    DefaultFilesOptions = { DefaultFileNames = new[] { "FrontPage.html" } }
});
app.UseMiddleware<UserLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();