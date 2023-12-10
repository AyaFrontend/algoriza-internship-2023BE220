using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Service.IServices;

namespace Vizeeta.Service.Services
{
    public class EmailService : IEmailService
    {

        private readonly IDatabase _db;
        public EmailService(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }

        public int GenerateRandom4DigitsCode()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ayamohamedabdelrahman868@gmail.com", "AyaMohamed123456");

            try
            {
                await client.SendMailAsync(from, to, subject, body);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }
        }
    }
}


