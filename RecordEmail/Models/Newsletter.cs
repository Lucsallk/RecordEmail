namespace RecordEmail.Models
{
    public class Newsletter
    {
        // Define as informações passadas na template do Informativo.
        public DateTime? DiaEnvio { get; set; }
        public List<String>? Atos { get; set; }
        public Newsletter(DateTime? diaEnvio = null, List<String>? atos = null) 
        {
            DiaEnvio = diaEnvio;
            Atos = atos;
        }
    }
}
