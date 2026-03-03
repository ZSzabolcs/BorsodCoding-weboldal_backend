using AuthApi.Services.Dtos;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using AuthApi.Services.Interfaces.IAuthService;
using AuthApi.Datas;
using AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class GoogleMail : ISend
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public GoogleMail(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public void SendMail(SendMailDto sendMailDto)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSettings:EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(sendMailDto.To));
            email.Subject = sendMailDto.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = sendMailDto.Body
            };


            using var smtp = new SmtpClient();

            smtp.Connect(_configuration.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(
                _configuration.GetSection("EmailSettings:EmailUserName").Value,
                _configuration.GetSection("EmailSettings:EmailPassword").Value
                );
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task<object> SendMailByUserName(SendMailByUserNameDto sendMailByUserNameDto)
        {
            if (sendMailByUserNameDto != null)
            {

                var user = await _context.applicationUsers.FirstOrDefaultAsync(u => u.NormalizedUserName == sendMailByUserNameDto.UserName.ToUpper());


                if (user.Email != null && user != null)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSettings:EmailUserName").Value));
                    email.To.Add(MailboxAddress.Parse(user.Email));
                    string szoveg = $"<h1>Üdvözöljük {sendMailByUserNameDto.UserName}!</h1>";
                    if (sendMailByUserNameDto.IsLogin)
                    {
                        email.Subject = "Sikeres bejelentkezés történt";
                        szoveg += "<p>Bejelentkezés történt, de Ön volt? Ha nem, akkor változtassa meg a jelszót azonnal!</p>";
                    }
                    else
                    {
                        email.Subject = "Sikeres regisztráció";
                        szoveg += "<p>Így hivatalosan fiókja lett a BorsodCoding-ban!</p>";
                    }

                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = szoveg

                    };

                    using var smtp = new SmtpClient();
                    smtp.Connect(_configuration.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);

                    smtp.Authenticate(
                        _configuration.GetSection("EmailSettings:EmailUserName").Value,
                        _configuration.GetSection("EmailSettings:EmailPassword").Value
                        );
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    return "Sikeres bejelentkezés vagy regisztráció";
                }

                return "Sikertelen bejelentkezés vagy regisztráció";
                
            }

            return "Nincs adat";

        }
    }
}
