using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace ESW_Shelter.Models
{
    public class NotificationSender
    {
        private readonly IConfiguration _configuration;
        private readonly EmailAddress From = new EmailAddress("eswG2_2018_2019@hotmail.com", "Grupo2");

        public NotificationSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PostMessage(string subjectTxt, string emailContent, string toSend, string name)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            List<EmailAddress> tos = new List<EmailAddress>
          {
              new EmailAddress(toSend,name)
          };

            var subject = subjectTxt;
            var htmlContent = emailContent;
            var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(From, tos, subject, "", htmlContent, false);
            /*var banner2 = new Attachment()
            {
                Content = Convert.ToBase64String(),
                Type = "image/png",
                Filename = "banner2.png",
                Disposition = "inline",
                ContentId = "Banner 2"
            };*/
            //msg.AddAttachment(banner2);
            /*msg.SetFooterSetting(
                     true,
                     "Some Footer HTML",
                     "<strong>Some Footer Text</strong>");*/
            //msg.SetClickTracking(true);
            var response = await client.SendEmailAsync(msg);

        }
    }
}
