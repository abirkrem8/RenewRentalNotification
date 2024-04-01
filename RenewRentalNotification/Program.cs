// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RenewRentalNotification;
using RenewRentalNotification.Logic.Shared;


// Generate fake appointments with exisiting clients and hair stylists at the hair salon for x days in advance. 

var env = "development";
var configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory() + "/conf/")
 .AddJsonFile($"appsettings.json")
 .AddJsonFile($"appsettings.{env}.json").Build();


var connectionString = configuration.GetConnectionString("DefaultConnection");
IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
    services.AddSingleton<IRenewRentalNotificationService, RenewRentalNotificationService>();
    //services.AddTransient<AppointmentScheduleHandler>();

}).Build();


  var service = _host.Services.GetRequiredService<IRenewRentalNotificationService>();
  int exitCode = service.Run();


  Environment.Exit(exitCode);
