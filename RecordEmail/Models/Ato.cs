using System.ComponentModel.DataAnnotations.Schema;

namespace RecordEmail.Models
{
    public class Ato
    {
        // Aproximação da tabela de Atos do LegisOn.
        public int Id { get; set; }

        public string Numero { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAto { get; set; }

        public DateTime DataPublicacao { get; set; }

        public bool Disponivel { get; set; }

        public string PalavrasChave { get; set; }

        public int IdUsuarioCriador { get; set; }

        public string Ementa { get; set; }

        public bool IsAtivo { get; set; }

        public int IdTipoAto { get; set; }

        public int? IdIniciativa { get; set; }

        public DateTime DataRepublicacao { get; set; }

        public string MensagemGovernamental { get; set; }

        public string NumeroOrigem { get; set; }

        public string Assinatura { get; set; }

        public int? IdTipoOrigem { get; set; }

        public int? Inteiro { get; set; }

        public int? IdSituacao { get; set; }

        public int? IdParecer { get; set; }

        [ForeignKey("IdParecer")]
        public virtual Ato Parecer { get; set; }
    }
}
