
using SendGrid;
using SendGrid.Helpers.Mail;
using Server.Models;

namespace Server.Services;

public class SendGridService : IEmailService
{
    private readonly SendGridClient _emailClient;
    private readonly string _client;

    public SendGridService()
    {
        _client = Environment.GetEnvironmentVariable("CLIENT")?? "";
        _emailClient = new(Environment.GetEnvironmentVariable("SENDGRID_API_KEY" ?? ""));
    }

    public async Task<bool> ConfirmRegisterAsync(User user, HashStorage storage)
    {
        var from = new EmailAddress("noreply@portfolio-project-odontologist.com", "Control Panel");
        var subject = "Control Panel - Confirm registration";
        var to = new EmailAddress(user.Email, user.Name);
        var plainTextContent = $"Confirm your registration by pasting the following link. If you did not request any registration, please ignore this email. {_client}/user/verify_registration?hash={storage.Hash}&userId={storage.UserId}&operation={Convert.ToInt32(storage.Operation)}";
        var htmlContent = $"<p>Confirm your registration by clicking the following link.</p><a href=\"{_client}/user/verify_registration?hash={storage.Hash}&userId={storage.UserId}&operation={Convert.ToInt32(storage.Operation)}\">Confirm your email</a><p>If you did not request any registration, please ignore this email.</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await _emailClient.SendEmailAsync(msg);
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<bool> ConfirmEmailAsync(User user, HashStorage storage)
    {
        var from = new EmailAddress("noreply@portfolio-project-odontologist.com", "Control Panel");
        var subject = "Control Panel - Confirm email change";
        var to = new EmailAddress(user.Email, user.Name);
        var plainTextContent = $"Confirm your email change by pasting the following link. If you did not request any email change, please ignore this email. {_client}/user/confirm_change_email?hash={storage.Hash}&userId={storage.UserId}&operation={Convert.ToInt32(storage.Operation)}";
        var htmlContent = $"<p>Confirm your email change by clicking the following link.</p><a href=\"{_client}/user/confirm_email_change?hash={storage.Hash}&userId={storage.UserId}&operation={Convert.ToInt32(storage.Operation)}\">Confirm your email</a><p>If you did not request any registration, please ignore this email.</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await _emailClient.SendEmailAsync(msg);
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<bool> ForgetPasswordAsync(User user, HashStorage storage)
    {
        var from = new EmailAddress("noreply@portfolio-project-odontologist.com", "Control Panel");
        var subject = "Control Panel - Forget password";
        var to = new EmailAddress(user.Email, user.Name);
        var plainTextContent = $"Reset your password by pasting the following link. If you did not request to change password, please ignore this email. {storage.Hash}";
        var htmlContent = $"<p>To reset your password click the following link.</p><a href=\"{_client}/user/reset_password?hash={storage.Hash}&userId={storage.UserId}&operation={Convert.ToInt32(storage.Operation)}\">Reset password</a><p>If you did not request to change password, please ignore this email.</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await _emailClient.SendEmailAsync(msg);
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}