using backend_api.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure mongoDb database
builder.Services.Configure<TweeterDatabaseSettings>(
    builder.Configuration.GetSection("TweeterDatabaseSettings"));
builder.Services.AddSingleton<ITweeterDatabaseSettings>(t => t.GetRequiredService<IOptions<TweeterDatabaseSettings>>().Value);
builder.Services.AddScoped<ITweetRepository, MongoTweeterRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
