using Microsoft.EntityFrameworkCore;
using Project.Manager.Api.Filter;
using Project.Manager.Api.Models;
using Project.Manager.Application;
using Project.Manager.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")  ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services
               .SetupData(connectionString)
               .SetupApplication();

builder.Services.AddControllers(option => {
    option.Filters.Add(typeof(ExceptionFilter));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();