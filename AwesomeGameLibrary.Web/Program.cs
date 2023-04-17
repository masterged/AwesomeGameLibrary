using AwesomeGameLibrary.Application;
using AwesomeGameLibrary.DAL.Contexts;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatr();
builder.Services.AddHealthCheck();

builder.Services
    .AddDbContext<AwesomeDbContext>((_, optionsBuilder) =>
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("AwesomeDbContext")));

builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();
app.AuditSetupOutput();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting()
    .UseEndpoints(config =>
        {
            config.MapHealthChecks("/health-process", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("process"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            config.MapHealthChecks("/health-disc", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("storage"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            config.MapHealthChecksUI();
        }
    );

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.Run();