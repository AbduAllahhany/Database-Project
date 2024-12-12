using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace hospital.management.system.BLL.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration config;

    public EmailSender(IConfiguration config)
    {
        this.config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var apiKey = config["SendGrid:SecretKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(config["Hospital:Email"], config["Hospital:Name"]);
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, config["Hospital:Name"], htmlMessage);
        var response = await client.SendEmailAsync(msg);
    }
}

