using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hangfire;
using InventoryManagementSystem.App.Config;
using InventoryManagementSystem.App.Data;
using InventoryManagementSystem.App.Extensions;
using InventoryManagementSystem.App.Features.CapServices;
using InventoryManagementSystem.App.Features.Common.ConsumeMessages;
using InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Middlewares;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var emailConfig = builder.Configuration
        .GetSection("EmailSettings")
        .Get<EmailConfiguration>();

builder.Services.AddHostedService<MessageConsumer>();

builder.Services.AddSingleton(emailConfig);

builder.Services.AddMediatR(AssemblyReference.Assembly);
builder.Services.AddTransient<CapConsumerService>();
builder.Services.AddTransient<ProductDecreasOrchConsumer>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // Register your Autofac module
        containerBuilder.RegisterModule(new AutofacModule());
    });
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
builder.Services.AddCap(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    config.UseEntityFramework<AppDbContext>();
    config.UseRabbitMQ(opt =>
    {
        opt.HostName = "localhost";
        opt.Port = 5672;
        opt.Password = "guest";
        opt.UserName = "guest";
        opt.ExchangeName = "cap.default.router";


    });
    config.UseDashboard();

});

builder.Services.AddHangfireServer();
builder.Services.AddCompressionServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

});

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
app.UseDeveloperExceptionPage();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = ""; // Set Swagger at root (optional)
});
app.UseSwaggerUI();
app.Run();
