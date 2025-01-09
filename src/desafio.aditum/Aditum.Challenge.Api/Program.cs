using Aditum.Challenge.CrossCutting.Model;
using Aditum.Challenge.CrossCutting.Mongo;
using Aditum.Challenge.CrossCutting.Repositories;
using Aditum.Challenge.CrossCutting.Services;

var builder = WebApplication.CreateBuilder(args);

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .AddRepositories()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMongo(applicationSettings!.MongoSettings!)
    .AddServicos();
    

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
