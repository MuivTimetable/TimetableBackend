﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace TimetableAPI.Services
{
    public class SMTPSender
    {
        public void Send(string email, int code, IOptions<SMTPConfig> _options)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("Test", _options.Value.EmailAdress));

            message.To.Add(MailboxAddress.Parse(email));
            
            message.Body = new TextPart("plain") { Text = code.ToString() };

            using (SmtpClient client = new SmtpClient())
            {
                try
                {
                    client.Connect(_options.Value.Host, _options.Value.Port, _options.Value.SSL);
                    client.Authenticate(_options.Value.EmailAdress, _options.Value.Password);
                    client.Send(message);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
                return;
            }
        }
    }
}
