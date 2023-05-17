using System.ComponentModel.DataAnnotations;

namespace RecordEmail.Models
{
    public class RegistroEndereco
    {
        // Requisição feita pra criação de email.
        // Pensei em mesclar com outro model pra diminuir a quantidade mas não sei se seraia o mais prudente.
        // A modificar ?
        [Required, EmailAddress]
        public string EnderecoEmail { get; set; } = string.Empty;
    }
}
