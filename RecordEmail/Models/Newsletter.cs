namespace RecordEmail.Models
{
    public class Newsletter
    {
        // Define as informações passadas na template do Informativo.
        public DateTime? DiaEnvio { get; set; }
        public List<string>? Atos { get; set; }
        public Newsletter(DateTime? diaEnvio = null, List<string>? atos = null) 
        {
            DiaEnvio = diaEnvio;
            Atos = atos;
        }
    }
}
