using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ESW_Shelter.Controllers
{
    public class MailSenderController : Controller
    {

        private readonly IConfiguration _configuration;

        public MailSenderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PostMessage()
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("test@example.com", "DX Team"),
                Subject = "Hello World from the SendGrid CSharp SDK!",
                PlainTextContent = "Hello, Email!",
                HtmlContent = "<strong>Hello, Email!</strong>"
            };
            msg.AddTo(new EmailAddress("gomesmike24@hotmail.com", "Test User"));

            var response = await client.SendEmailAsync(msg);
        }


    }
}