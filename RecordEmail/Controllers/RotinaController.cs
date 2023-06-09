﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordEmail.Context;
using RecordEmail.Models;
using RecordEmail.Services;

namespace RecordEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotinaController : ControllerBase
    {
        private readonly EmailCadastroBdContext _context;
        private readonly IEmailServices _mail;

        public RotinaController(EmailCadastroBdContext context, IEmailServices mail)
        {
            _context = context;
            _mail = mail;
        }

        [HttpPost("EnviarNewsletter")]
        public async Task<IActionResult> EnviarNewsletterEmailComTemplate()
        {

            // Colhe todos os endereços a serem enviados um email

            List<Email> emails = await _context.Emails.Where(e => e.Ativo == true).ToListAsync();

            List<string> emailsDestinatarios = new List<string>();
            
            foreach (var email in emails)
            {
                emailsDestinatarios.Add(email.EnderecoEmail);
            }
            
            // Colhe os atos a serem inseridos no email
            // Somente os atos que tiverem sido modificados no dia anterior ao envio.

            List<Ato> atosNovos = await _context.Atos.Where(e => e.DataPublicacao == DateTime.Today.AddDays(-1)).ToListAsync();

            List<Ato> atosAlterados = await _context.Atos.Where(e => e.DataRepublicacao == DateTime.Today.AddDays(-1)).ToListAsync();

            var atos = new List<String>();
            
            /* Estudar depois
            atos.AddRange(atosNovos);
            atos.AddRange(atosAlterados);
            Console.WriteLine(atos);
            */
            
            foreach (var ato in atosNovos)
            {
                atos.Add(ato.Numero); 
            }

            foreach (var ato in atosAlterados)
            {
                atos.Add(ato.Numero);
            }

            // Atrela as informações colhidas ao models do newsletter ( aceito sugestões )

            Newsletter newsletter = new Newsletter(DateTime.Now, atos);

            // Preenche os dados necessários para realizar o envio de email

            EmailData emailData = new EmailData(
                emailsDestinatarios,
                "Newsletter Validacao",
                _mail.GetEmailTemplate("newsletter_template", newsletter));

            bool sendResult = await _mail.SendEmailAsync(emailData, new CancellationToken());

            if (sendResult)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent using template.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}
