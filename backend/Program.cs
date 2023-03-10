using backend.Repositories;
using MongoDB.Driver;
using backend.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using OpenAI_API;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Net.Mime;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDatabaseSettings)).Get<MongoDbSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com",
                                              "http://localhost:3000",
                                              "http://44.213.112.252:3000",
                                              "http://35.175.110.252:3000")                                                  
                                                  .AllowAnyHeader()                                                    
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                      });
});

//Load enviroment variables
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
YourApplication.DotEnv.Load(dotenv);

// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
builder.Services.AddSingleton<IChatRepository, MongoDbChatRepository>();
builder.Services.AddSingleton<OpenAIAPI>(ServiceProvider => {
    return new OpenAIAPI();
});
builder.Services.AddSingleton<IMongoClient>(ServiceProvider => {
    return new MongoClient(mongoDbSettings.ConnectionString);
});
builder.Services.AddControllers(options => 
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
                .AddMongoDb(mongoDbSettings.ConnectionString, 
                            name: "mongodb", 
                            timeout: TimeSpan.FromSeconds(3),
                            tags: new[] { "ready" });
                

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async(context, report) =>
    {
        var result = JsonSerializer.Serialize(
            new{
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                    duration = entry.Value.Duration.ToString()
                })
            }
        );

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions{
    Predicate = (_) => false
});

app.Run();
