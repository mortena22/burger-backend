using Microsoft.EntityFrameworkCore;
using ReviewApi.Persistence.Context;
using ReviewApi.Persistence.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<ReviewContext>(options => options.UseSqlite(Environment.GetEnvironmentVariable("connection_string") ?? "Data Source=Review.db"));
builder.Services.AddScoped<IReviewDataService, ReviewDataService>();

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

