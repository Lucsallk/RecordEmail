using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using RazorEngineCore;
using RecordEmail.Models;
using RecordEmail.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using RecordEmail.Context;

namespace RecordEmail.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfigs _configs;
        private readonly EmailCadastroBdContext _context;

        public EmailServices(IOptions<EmailConfigs> configs, EmailCadastroBdContext context)
        {
            _configs = configs.Value;
            _context = context;
        }
                
        /* Faz o envio do email em si.
         * 
         * Problemas não solucionados: porta em uso não aceita criptografia e ao usar a porta criptografada 
         * a conexão não consegue ser feita de maneira alguma. Me relataram que poderia ser algo a ver com a 
         * configuração da porta, pois o mesmo erro já ocorreu em outras aplicações.
        */ 

        public async Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct = default)
        {
            // Eventualmente mudar a lista de endereços de emails para bcc,
            // pois impede que apareça os vários usuários ao ler o email.

            try
            {
                #region Remetende / Destinatario

                // Adiciona os endereços a lista de destinatários.
                var enderecosEmails = await _context.Emails.ToListAsync();

                foreach (var email in enderecosEmails)
                {
                    if(email.Ativo == true) emailData.Bcc.Add(email.EnderecoEmail);
                }

                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress(_configs.DisplayName, emailData.From ?? _configs.From));
                mail.Sender = new MailboxAddress(emailData.DisplayName ?? _configs.DisplayName, emailData.From ?? _configs.From);

                foreach (string mailAddress in emailData.Bcc)
                {
                    mail.Bcc.Add(MailboxAddress.Parse(mailAddress));
                }

                // Configurações para especificar o campo de ReplyTo.
                if (!string.IsNullOrEmpty(emailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(emailData.ReplyToName, emailData.ReplyTo));

                if (emailData.To != null)
                {
                    // Busca apenas valores não nulos. x = value of address.
                    foreach (string mailAddress in emailData.To.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.To.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                if (emailData.Cc != null)
                {
                    foreach (string mailAddress in emailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                #endregion

                #region Conteudo

                // Formação do corpo do Email.
                var body = new BodyBuilder();
                mail.Subject = emailData.Subject;
                body.HtmlBody = emailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Enviar Email

                // Atualmente não funciona com criptografia.

                using var smtp = new SmtpClient();

                if (_configs.UseSSL)
                {
                    await smtp.ConnectAsync(_configs.Host, _configs.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_configs.UseStartTls)
                {
                    await smtp.ConnectAsync(_configs.Host, _configs.Port, SecureSocketOptions.StartTlsWhenAvailable, ct);
                }

                await smtp.AuthenticateAsync(_configs.UserName, _configs.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                return true;

                #endregion
            }
            catch (Exception)
            {
                return false;
            }
        }
        // ---- Email Template ----
        public string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel)
        {
            // Carrega a teplate enviada. emailTemplate = nome da template.
            string mailTemplate = LoadTemplate(emailTemplate);

            // Copila a template utilizando das funções do Razor. Basicamente habilita c# em html
            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

            // Se todos os dados na template forem corretamente carregados retorna true.
            return modifiedMailTemplate.Run(emailTemplateModel);
        }
        public string LoadTemplate(string emailTemplate)
        {
            // Encontra a template e prepara ela para uso.
            string templatePath = Path.Combine("Files/EmailTemplates", $"{emailTemplate}.cshtml");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}
