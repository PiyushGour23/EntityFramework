using Billboard.Helper;

namespace Billboard.Service
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
