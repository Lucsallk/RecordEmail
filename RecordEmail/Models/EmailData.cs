namespace RecordEmail.Models
{
    public class EmailData
    {
        // Definição para todos os dados que vão compor o que é o email.

        // Destinatário
        public List<string> To { get; set; }
        public List<string> Bcc { get; }

        public List<string> Cc { get; }

        // Remetente
        public string? From { get; }

        public string? DisplayName { get; }

        public string? ReplyTo { get; }

        public string? ReplyToName { get; }

        // Conteudo
        public string Subject { get; }

        public string? Body { get; }

        public EmailData(List<string> bcc, string subject, string? body = null, string? from = null, string? displayName = null, string? replyTo = null, string? replyToName = null, List<string>? to = null, List<string>? cc = null)
        {
            // Destinatário
            Bcc = bcc;
            To = to ?? new List<string>();
            Cc = cc ?? new List<string>();

            // Remetente
            From = from;
            DisplayName = displayName;
            ReplyTo = replyTo;
            ReplyToName = replyToName;

            // Conteudo
            Subject = subject;
            Body = body;
        }
    }
}
