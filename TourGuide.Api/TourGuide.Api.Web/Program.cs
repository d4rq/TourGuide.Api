using Microsoft.EntityFrameworkCore;
using TourGuide.Api.Web;
using TourGuide.Api.Web.Abstractions;
using TourGuide.Api.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

services.Configure<EmailOptions>(builder.Configuration.GetSection("Mail"));

services.AddScoped<IEmailService, EmailService>();

services.AddControllers();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapControllers();

app.Run();