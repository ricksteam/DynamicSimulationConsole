using System.Text.Json.Serialization;
using DynamicSimulationConsole.RoadGraph.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions((options) => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRoadGraphRepository, InMemoryRoadGraphRepository>();
builder.Services.AddSingleton<IConvoyRepository, InMemoryConvoyRepository>();

builder.Services.AddCors();

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
app.UseCors((c) => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().DisallowCredentials());

app.Run();