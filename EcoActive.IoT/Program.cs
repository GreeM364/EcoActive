using EcoActive.BLL.Infrastructure;
using EcoActive.IoT;
using EcoActive.IoT.Hubs;
using EcoActive.IoT.Observers;
using EcoActive.IoT.Observers.IObservers;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(AutomapperIoTProfile));
builder.Services.AddBusinessLogicLayer(builder.Configuration);

builder.Services.AddSingleton<IRealTimeEnvironmentalIndicatorsObserver, RealTimeEnvironmentalIndicatorsObserver>();
builder.Services.AddSingleton<IAverageEnvironmentalIndicatorsObserver, AverageEnvironmentalIndicatorsObserver>();
builder.Services.AddSingleton<ICriticalIndicatorsObserver, CriticalIndicatorsObserver>();
builder.Services.AddHostedService<MqttService>();


// Configure the HTTP request pipeline.
var app = builder.Build();
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
