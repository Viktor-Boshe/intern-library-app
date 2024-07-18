
namespace LibraryApiService.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailSender emailSender);
    }
}
