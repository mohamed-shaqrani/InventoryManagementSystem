using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hangfire;
using InventoryManagementSystem.App.Config;
using InventoryManagementSystem.App.Data;
using InventoryManagementSystem.App.Extensions;
using InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Middlewares;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var emailConfig = builder.Configuration
        .GetSection("EmailSettings")
        .Get<EmailConfiguration>();

builder.Services.AddHostedService<MessageConsumer>();

builder.Services.AddSingleton(emailConfig);

builder.Services.AddMediatR(AssemblyReference.Assembly);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new AutofacModule());
});

builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"),
        new Hangfire.SqlServer.SqlServerStorageOptions
        {
            QueuePollInterval = TimeSpan.FromSeconds(5),
            CommandTimeout = TimeSpan.FromMinutes(1),

        });
});
builder.Services.AddHangfireServer();
builder.Services.AddCompressionServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddMemoryCache();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHangfireDashboard("/hangfire");
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
MappingExtensions.Mapper = app.Services.GetRequiredService<IMapper>();

app.UseAuthorization();
#region Custom Middleware
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
#endregion
app.MapControllers();

app.Run();
