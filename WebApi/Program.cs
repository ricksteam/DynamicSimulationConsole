using System.Text.Json.Serialization;
using System.Xml.Linq;
using DynamicSimulationConsole.DataAccessLayer.Repositories;
using Engines;
using Shared.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions((options) => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var nodes = OsmParser.ExtractNodesAndBridges(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-Merge-v1-1.osm",
    out var bridges);
var osmData = new OsmData
{
    Nodes = nodes,
    Bridges = bridges
};

// var bridges = PbiParser.GetBridges(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-merge-v1-1-1.pbf");
// var osmData = new OsmData()
// {
//     Nodes = new List<OsmNode>(),
//     Bridges = bridges
// };

builder.Services.AddSingleton<OsmData>(osmData);
builder.Services.AddSingleton<IConvoyRepository, MongoConvoyRepository>();
builder.Services.AddSingleton<IRouteRepository, MongoRouteRepository>();

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