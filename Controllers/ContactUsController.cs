using College_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace College_Portal.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(RegModel model)
        {
            var message = new MimeMessage();
            var message1 = new MimeMessage();
            message.From.Add(new MailboxAddress("SRF University", "@gmail.com"));
            message1.From.Add(new MailboxAddress("SRF University", "@gmail.com"));
            message.To.Add(new MailboxAddress("receiptent", model.mailId));
            message1.To.Add(new MailboxAddress("receiptent", ""));
     
            message.Subject = "Thanks For writing to SRF University";
            message1.Subject = $" {model.Name} contacted SRF University";
            message.Body = new TextPart("plain")
            {
                Text = $"Hi {model.Name}, We from SRF university will try our best to reach you ASAP on this issue written to us."
            };
            message1.Body = new TextPart("plain")
            {
                Text=$"Hi SRF University, Name: {model.Name} MailId : {model.mailId}  phone : {model.MNo} have contacted your site."
            
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("", "");
                client.Send(message);
                client.Disconnect(true);
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("@gmail.com", "");
                client.Send(message1);
                client.Disconnect(true);
            }


            return RedirectToAction("Index", "Home");
        }

    }
}
