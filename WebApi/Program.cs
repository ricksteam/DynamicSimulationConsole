using System.Text.Json.Serialization;
using System.Xml.Linq;
using DynamicSimulationConsole.DataAccessLayer.Repositories;
using Engines;
using Engines.Interface;
using Shared.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions((options) => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var nodes = OsmParser.ExtractNodesAndBridges(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-Merge-v1-1.osm",
//     out var bridges);
// var osmData = new OsmData
// {
//     Nodes = nodes,
//     Bridges = bridges
// };

var (nodes, edges) = PbiParser.LoadDataFromPBF(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-merge-v1-1-1.pbf");
var osmData = new PbiData()
{
    Nodes = nodes,
    Edges = edges
};

builder.Services.AddSingleton<PbiData>(osmData);
builder.Services.AddSingleton<IConvoyRepository, MongoConvoyRepository>();
builder.Services.AddSingleton<IRouteRepository, MongoRouteRepository>();
var simEngine = new SimulationEngine(osmData);
builder.Services.AddSingleton<ISimulationEngine>(simEngine);
builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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