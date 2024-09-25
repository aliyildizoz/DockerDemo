using AspNetCoreElasticsearch.Services;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var userName = builder.Configuration.GetSection("ElasticsearchOption")["Username"];
var password = builder.Configuration.GetSection("ElasticsearchOption")["Password"];
var settings = new ElasticsearchClientSettings(new Uri(builder.Configuration.GetSection("ElasticsearchOption")["Url"]!)).Authentication( new BasicAuthentication(userName!,password!));

var client= new ElasticsearchClient(settings);

builder.Services.AddSingleton(client);

builder.Services.AddScoped<ElasticsearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();
app.MapControllers();


app.Run();
