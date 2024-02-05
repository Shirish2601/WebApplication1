using AssetManagement.Api.Models;
using AssetManagement.Api.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddSingleton<IFileReader>(option => new TextFileReader(builder.Configuration.GetSection("FileSettings")["FilePath"]));

builder.Services.Configure<MachineDataStoreSetting>(builder.Configuration.GetSection(nameof(MachineDataStoreSetting)));
builder.Services.AddSingleton<IMachineDataStoreSetting>(option => option.GetRequiredService<IOptions<MachineDataStoreSetting>>().Value);
builder.Services.AddSingleton<IMongoClient>(option => new MongoClient(builder.Configuration.GetSection("MachineDataStoreSetting")["MongoConnectionString"]));

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
