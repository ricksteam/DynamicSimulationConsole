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

//var osmDocument = XDocument.Load(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-Merge-v1-1.osm");
//var nodes = OsmParser.ExtractNodes(osmDocument);
//var bridges = OsmParser.ExtractBridges(osmDocument, nodes);
var nodes = OsmParser.ExtractNodesAndBridges(Directory.GetCurrentDirectory() + "\\..\\OSM\\NE-Merge-v1-1.osm",
    out var bridges);
var osmData = new OsmData
{
    Nodes = nodes,
    Bridges = bridges
};

builder.Services.AddSingleton<OsmData>(osmData);
builder.Services.AddSingleton<IConvoyRepository, MongoConvoyRepository>();

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