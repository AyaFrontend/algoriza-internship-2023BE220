using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Service.IServices
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string from, string to, string subject, string body);
        public int GenerateRandom4DigitsCode();
    }
}
