using BackEnd;
using BackEnd.Controllers;
using BackEnd.Data;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Define Connection string to SQLite db.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=conferences.db";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSqlite<BackEnd.Data.ApplicationDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints( endpoints => {
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});





app.Run();
