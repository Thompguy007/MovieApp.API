using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer;
using MovieApp.BusinessLayer.Services;
using MovieApp.DataLayer;
using MovieApp.DataLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MovieContext
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieDatabase")));

// Register BookmarkService
builder.Services.AddScoped<BookmarkService>();

// Register UserService
builder.Services.AddTransient<UserService>();

builder.Services.AddScoped<BookmarkBusinessService>();
builder.Services.AddScoped<UserBusinessService>();

builder.Services.AddScoped<UserRatingBusinessService>();
builder.Services.AddScoped<UserRatingService>();

builder.Services.AddScoped<SearchHistoryBusinessService>();
builder.Services.AddScoped<SearchHistoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
