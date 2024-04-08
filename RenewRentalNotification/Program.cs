// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RenewRentalNotification;
using RenewRentalNotification.Logic.Shared;
using RenewRentalNotification.Logic.FindMoveOutTenants;
using RenewRentalNotification.Logic.SendEmailToTenant;
using AutoMapper;
using RenewRentalNotification.Logic.SendMoveOutListToManagement;


var env = "development";
var configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory() + "/conf/")
 .AddJsonFile($"appsettings.json")
 .AddJsonFile($"appsettings.{env}.json").Build();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
    mc.AddProfile(new FindMoveOutTenantsMappingProfile());
    mc.AddProfile(new SendEmailToTenantMappingProfile());
    mc.AddProfile(new SendMoveOutListToManagementMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();


var connectionString = configuration.GetConnectionString("DefaultConnection");
IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
    services.AddMemoryCache();
    services.AddSingleton(mapper);

    services.AddSingleton<ILoadMemoryCache, LoadMemoryCache>();
    services.AddTransient<FindMoveOutTenantsHandler>();
    services.AddTransient<SendEmailToTenantHandler>();
    services.AddTransient<SendMoveOutListToManagementHandler>();
    services.AddSingleton<IRenewRentalNotificationService, RenewRentalNotificationService>();

}).Build();

var loadMemoryCache = _host.Services.GetRequiredService<ILoadMemoryCache>();
loadMemoryCache.Handle(configuration);


var service = _host.Services.GetRequiredService<IRenewRentalNotificationService>();
int exitCode = service.Run();


Environment.Exit(exitCode);

