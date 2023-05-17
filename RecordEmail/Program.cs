using MailKit;
using RecordEmail.Configurations;
using RecordEmail.Context;
using RecordEmail.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmailCadastroBdContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<EmailConfigs>(builder.Configuration.GetSection(nameof(EmailConfigs)));
builder.Services.AddTransient<IEmailServices, EmailServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
