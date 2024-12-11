using InveonBootcamp.Models;
using InveonBootcamp.Models.Caching;
using InveonBootcamp.Models.Repositories;
using Microsoft.EntityFrameworkCore; // BookRepository'nin namespace'i

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Books_";
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();