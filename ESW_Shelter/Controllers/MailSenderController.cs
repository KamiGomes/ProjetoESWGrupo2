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

        public async Task PostMessage(string emailToSend, string nameToSend, int id)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            string link = String.Format("< h2 >< a href=\"https://eswshelter.azurewebsites.net/Users/ConfirmEmail/{0}\" > Concluir Registo </ a ></ h2 > ", id);
                var msg = new SendGridMessage()
            {
                From = new EmailAddress("eswg21819@gmail.com", "ESW - Grupo 2"),
                Subject = "Boas-vindas do Grupo 2 ao Abrigo dos Animais AAAMoita!",
                HtmlContent = @"<div style= ""border - style: groove;border - color: orange;border - width: 7px;text - align: center;margin: 5 % 0 0 0; "">< h1 style = ""color: darkorange;"" > Bem vindos ao abrigo dos animais AAAMoita!</ h1 >< img src = ""pic_trulli.jpg"" alt = ""Trulli"" width = ""100"" height = ""100"" style = ""position: absolute; top: 0; left: 10%;"">< p > O Grupo 2 de ESW, da escola IPS, agradeçe por registar no site e querer ajudar os nossos patudos!</ p >< p > Para concluir o registo, por favor clique no link abaixo para confirmar o email.</ p >"+ 
                link + "< p > Para qualquer dúvida e questão, pode utilizar este email para comunicar connosco!</ p >< h3 > Vemo - nos em breve!</ h3 ></ div >",
            };
            msg.AddTo(new EmailAddress(emailToSend, nameToSend));

            var response = await client.SendEmailAsync(msg);
        }


    }
}