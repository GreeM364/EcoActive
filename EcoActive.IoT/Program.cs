using EcoActive.BLL.Infrastructure;
using EcoActive.IoT;
using EcoActive.IoT.Hubs;
using EcoActive.IoT.Observers;
using EcoActive.IoT.Observers.IObservers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessLogicLayer(builder.Configuration);

builder.Services.AddSingleton<IRealTimeEnvironmentalIndicatorsObserver, RealTimeEnvironmentalIndicatorsObserver>();
builder.Services.AddSingleton<IAverageEnvironmentalIndicatorsObserver, AverageEnvironmentalIndicatorsObserver>();
builder.Services.AddSingleton<ICriticalIndicatorsObserver, CriticalIndicatorsObserver>();
builder.Services.AddHostedService<MqttService>();

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

app.MapHub<EnvironmentalIndicatorHub>("/environmentalindicatorhub");

app.Run();
