namespace RecordEmail.Models
{
    public class VerificarEmail
    {
        // Model para envio de dados na template de conferir email.
        // Posteriormente testar se é possível reutilizar RegistroEndereco
        public string Email { get; set; }
        public string Token { get;set; }
    }
}
