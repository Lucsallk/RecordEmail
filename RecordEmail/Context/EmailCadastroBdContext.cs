using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RecordEmail.Models;

namespace RecordEmail.Context
{
    public class EmailCadastroBdContext : DbContext
    {
        public EmailCadastroBdContext(DbContextOptions<EmailCadastroBdContext> options) : base(options)
        {}

        public DbSet<Email> Emails { get; set; }

        public DbSet<Ato> Atos { get; set; }

        // Conectar direto com o bd.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseMySql(Environment.GetEnvironmentVariable("EMAIL_CADASTRO_TESTE"), new MySqlServerVersion(new Version(8, 0, 32)), options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
            }
        }
    }
}
