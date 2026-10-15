using MiniRobot.Infrastructure;
using MiniRobot.Worker;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MiniRobot")
    ?? throw new InvalidOperationException("Не задана строка подключения ConnectionStrings:MiniRobot");

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();