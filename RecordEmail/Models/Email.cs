using System.ComponentModel.DataAnnotations;

namespace RecordEmail.Models
{
    public class Email
    {
        // Define a tabela para pessoas cadastradas no Informativo.
        [Key]
        public int IdEmail { get; set; }
        [Required]
        public string EnderecoEmail { get; set; } = string.Empty;
        [Required]
        public bool Verificado { get; set; } = false;
        [Required]
        public bool Ativo { get; set; } = false;
        public string? TokenDeVerificacao { get; set; }
        public DateTime RegistradoEm { get; set; }
        public DateTime VerificadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }
    }
}
