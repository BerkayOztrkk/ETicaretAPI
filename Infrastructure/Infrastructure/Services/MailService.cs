using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate,string username)
        {
          string mail=$"Merhaba {username}  <br>+{orderDate} tarihinde vermiş olduğunuz{orderCode} kodlu siparişiniz kargoya verilmiştir.";
            await SendMailAsync(to, "Sipariş tamamlandı.", mail);
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml=isBodyHtml;
            foreach(var to  in tos) 
                mail.To.Add(to);
            mail.Subject=subject;
            mail.Body=body;
            mail.From=new(_configuration["Mail:Username"], "ZB Ticaret",System.Text.Encoding.UTF8);
            SmtpClient smtp = new();
            smtp.Credentials=new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port=123;
            smtp.EnableSsl = true;
            smtp.Host="localhost";
            await smtp.SendMailAsync(mail);

        }

        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {
           StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba <br> Aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong> <a target=\"_blank\" href=\"/");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Şifrenizi güncellemek için tıklayınız. </a> </strong> <br> <br> <span style=\"font-size:15px;\">Eğer talep sizin tarafınızdan gerçekleştirilmediyse bu maili önemsemeyiniz.</span>");
         await   SendMailAsync(to,"Şifre Yenileme Talebi",mail.ToString());
          

        }
    }
}
