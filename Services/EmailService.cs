using System;
using System.Net.Mail;
using System.Threading.Tasks;
using RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailMessage _mailMessage;
        public EmailService()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.UseDefaultCredentials = true;
            _smtpClient.Host = "Smtp.sendgrid.com";
            _smtpClient.Port = 25;
            _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress("respweb@respaunce.com", "Respaunce", System.Text.Encoding.UTF8);
            _smtpClient.Credentials = new System.Net.NetworkCredential("apikey", "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");
        }

        public async Task SendAccessGrantedMail(string name, string email, string password, string companyName)
        {
            _mailMessage.Subject = Convert.ToString("Adgang til Respaunce® CSR Management Tool");
            _mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.Body = "<table width='600' border='0' cellspacing='0' cellpadding='0' style='border:5px solid green;'><tr><td align='center' valign='left' ><table width='520' border='0'  cellpadding='0' cellspacing='0'> <tr>  <td style='font-size:16px;font-family:Arial, Helvetica, sans-serif;color:#595959; line-height:140%;'> <p>Hej<strong> " + name + "</strong>,&nbsp;</p><p> Velkommen til Respaunce&reg;, <strong> " + companyName + " </strong> 's CSR Management Tool, som administrerer og rapporterer virksomhedens  CSR-indsatser.<br><br>Du får adgang til Respaunce® via dette link::<br><a href='http://reporting.respaunce.com' > http://reporting.respaunce.com</a></p> Bruger:<strong> &nbsp; " + email + "" + " </strong><br>Password:<strong> " + password + "" + " </strong><p> God fornøjelse med Respaunce®. </p><p> Hvis du har problemer med at komme på Respaunce® eller spørgsmål, så kontakt os på tlf. 0045 73 709 708 eller e-mail til  <a href='mailto:support@respaunce.com' >support@respaunce.com </a>.</p><p> Med venlig hilsen <br>Respaunce® Teamet <br><br></p></td></tr></table></td></tr></table>";
            _mailMessage.To.Add(email);
            await _smtpClient.SendMailAsync(_mailMessage);
        }

        public async Task SendSupplierAccessGrantedMail(string email, string password, string companyName)
        {
            _mailMessage.Subject = Convert.ToString("Adgang til Respaunce® CSR Management Tool");
            _mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.Body = "<table width='600' border='0' cellspacing='0' cellpadding='0' style='border:5px solid green;'><tr><td align='center' valign='left' ><table width='520' border='0'  cellpadding='0' cellspacing='0'> <tr>  <td style='font-size:16px;font-family:Arial, Helvetica, sans-serif;color:#595959; line-height:140%;'> <p>Hej<strong> " + email + "</strong>,&nbsp;</p><p> Velkommen til Respaunce&reg;, <strong> " + companyName + " </strong> 's CSR Management Tool, som administrerer og rapporterer virksomhedens  CSR-indsatser.<br><br>Du får adgang til Respaunce® via dette link::<br><a href='http://reporting.respaunce.com' > http://reporting.respaunce.com</a></p> Bruger:<strong> &nbsp; " + email + "" + " </strong><br>Password:<strong> " + password + "" + " </strong><p> God fornøjelse med Respaunce®. </p><p> Hvis du har problemer med at komme på Respaunce® eller spørgsmål, så kontakt os på tlf. 0045 73 709 708 eller e-mail til  <a href='mailto:support@respaunce.com' >support@respaunce.com </a>.</p><p> Med venlig hilsen <br>Respaunce® Teamet <br><br></p></td></tr></table></td></tr></table>";
            _mailMessage.To.Add(email);
            await _smtpClient.SendMailAsync(_mailMessage);
        }
    }
}