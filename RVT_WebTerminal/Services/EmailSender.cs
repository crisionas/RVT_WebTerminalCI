using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using RVT_WebTerminal.Models;

namespace RVT_WebTerminal.Services
{
    public static class EmailSender
    {
        public static void SendContactMessage(ContactModel model)
        {
            try
            {
                // Who send?
                MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
                string username = "rvtvote@gmail.com";
                //Change your password
                string password = "Ialoveni1";
                // where to send?
                MailAddress To = new MailAddress("rvtvote@gmail.com");
                MailMessage msg = new MailMessage(From, To);
                msg.Subject = $"RVT | ContactForm - {model.Email} ";
                msg.Body = "Nume: " + model.Name + "  " + "| Email: " + model.Email + "  " + "| Mesaj: " + model.Message;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(username, password);
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            catch
            {
            }
        }
    }
}
