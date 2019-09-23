using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walfrido.Service.FIP
{
    class Email
    {
        public const string SMTP = "smtp.visomes.com.br";
        public const int SMTP_PORT = 587;
        public const string FROM = "noreplay@visomes.com.br";
        public const bool SSL = false;
        public const bool IS_HTML = true;
        public const string PWD = "160714";
        public const string FROM_TEXT = "<noreplay@visomes.com.br>";
        private List<string> destinatarioList;
        private string body;
        private string title;

        public Email(List<string> destinatarioList, string body, string title)
        {
            this.destinatarioList = destinatarioList;
            this.body = body;
            this.title = title;
        }

        public void SendMail()
        {
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();

            try
            {
                //Adiciona Destinátarios
                foreach (String destinatario in this.destinatarioList)
                {
                    mailMsg.To.Add(new System.Net.Mail.MailAddress(destinatario));
                }

                //Configuração da conta de email

                //Remetente
                mailMsg.From = new System.Net.Mail.MailAddress(FROM_TEXT);

                //smtp
                System.Net.Mail.SmtpClient smtpCliente = new System.Net.Mail.SmtpClient(SMTP, SMTP_PORT);
                smtpCliente.Credentials = new System.Net.NetworkCredential(FROM, PWD);
                smtpCliente.Host = SMTP;
                smtpCliente.Port = SMTP_PORT;
                smtpCliente.EnableSsl = SSL;

                //especifica se é em html
                mailMsg.IsBodyHtml = IS_HTML;

                //define o assunto
                mailMsg.Subject = title;

                //corpo do email
                mailMsg.Body = body;

                //envia o email
                smtpCliente.Send(mailMsg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
