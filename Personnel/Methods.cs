using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    public class Methods
    {
        public static string StringToHash(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(str);
            byte[] hash = md5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            foreach (var a in hash) sb.Append(a.ToString("X2"));
            return sb.ToString();
        }
        public static bool SendMailRecovery(string Mail,int Code)
        {
            try
            {
                MailAddress from = new MailAddress("gr603_boani@mail.ru", "Кадры");
                MailAddress to = new MailAddress(Mail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Востановление пароля";
                m.Body = $"<h2>Код для восстановления пароля: {Code}</h2>";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                smtp.Credentials = new NetworkCredential("gr603_boani@mail.ru", "7pBx53tiX2BKtHSfxzY7");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
