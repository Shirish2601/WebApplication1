using AssetManagement.Api.Models;
using AssetManagement.Api.MongoDB;
using AssetManagement.Api.MongoDBModels;
using AssetManagement.Api.Repository;
using AssetManagement.Api.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMachineRepository, MongoDbRepository>();
builder.Services.AddScoped<IMachineService, MongoDbService>();

builder.Services.AddSingleton<IFileReader>(option => new TextFileReader(builder.Configuration.GetSection("FileSettings")["FilePath"]));

builder.Services.Configure<MachineDataStoreSetting>(builder.Configuration.GetSection(nameof(MachineDataStoreSetting)));
builder.Services.AddSingleton<IMachineDataStoreSetting>(option => option.GetRequiredService<IOptions<MachineDataStoreSetting>>().Value);
builder.Services.AddSingleton<IMongoClient>(option => new MongoClient(builder.Configuration.GetSection("MachineDataStoreSetting")["MongoConnectionString"]));

builder.Services.AddSwaggerGen(option =>
{
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    option.IncludeXmlComments(xmlCommentFilePath);
});

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

app.Run();
