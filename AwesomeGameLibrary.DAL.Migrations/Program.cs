using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(x =>
        x.AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables())
    .ConfigureServices((context, collection) =>
    {
        collection.AddDbContext<AwesomeDbContext>(builder =>
            builder.UseSqlServer(context.Configuration.GetConnectionString("AwesomeDbContext"), 
                x => x.MigrationsAssembly("AwesomeGameLibrary.DAL.Migrations")));
        collection.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();