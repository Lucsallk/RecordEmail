using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using RecordEmail.Context;
using RecordEmail.Models;
using RecordEmail.Services;
using System.Security.Cryptography;

namespace RecordEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailCadastroBdContext _context;
        private readonly IEmailServices _mail;
        public EmailController(EmailCadastroBdContext context, IEmailServices mail)
        {
            _context = context;
            _mail = mail;
        }

        [HttpPost("CadastrarEmail")]
        public async Task<IActionResult> CadastrarEmail(RegistroEndereco request)
        {
        
            if(_context.Emails.Any(u => u.EnderecoEmail.Equals(request.EnderecoEmail)))
            {
                return BadRequest("Endereço já cadastrado.");
            }

            // adicionar validação de regex aqui ??
            // Salva os dados no banco.
            var email = new Email
            {
                EnderecoEmail = request.EnderecoEmail,
                RegistradoEm = DateTime.Now,
                TokenDeVerificacao = CreateRandomToken()
            };

            // Envio do email de verificação
            var emailVerificacao = new VerificarEmail
            {
                Email = request.EnderecoEmail,
                Token = email.TokenDeVerificacao
            };

            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            // Enviar email de verificação
            await EnviarEmailComTemplate(emailVerificacao);

            return Ok("Endereço cadastrado!");
        }

        [HttpPost("EnviarEmail")]
        public async Task<IActionResult> SendMailAsync(EmailData emailData)
        {
            bool result = await _mail.SendEmailAsync(emailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Email enviado com sucesso.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu. O email não pode ser enviado.");
            }
        }

        [HttpPost("EnviarEmailComTemplate")]
        public async Task<IActionResult> EnviarEmailComTemplate(VerificarEmail verificarEmail)
        {
            EmailData emailData = new EmailData(
                new List<string> { verificarEmail.Email },
                "Email com template teste",
                _mail.GetEmailTemplate("verificar_email", verificarEmail));

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

        [HttpPost("EmailVerificar")]
        public async Task<IActionResult> EmailVerificacao(string token)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(u => u.TokenDeVerificacao == token);
            
            if (email == null)
            {
                return BadRequest("Token Invalido - Email Não encontrado");
            }

            email.Ativo = true;
            email.Verificado = true;
            email.VerificadoEm = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("Email verificado com sucesso!");
        }

        [HttpPost("EmailDesinscrever")]
        public async Task<IActionResult> EmailDesinscrever(string enderecoEmail)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(u => u.EnderecoEmail == enderecoEmail);

            if (email == null)
            {
                return BadRequest("Token Invalido - Email Não encontrado");
            }

            email.Ativo = false;
            await _context.SaveChangesAsync();

            return Ok("Email desinscrito com sucesso!");
        }
    
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
