using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Online_Shopping.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext< ApplicationContext> (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
    b => b.MigrationsAssembly("Online_Shopping")));

builder.Services.ConfigureRepository();
builder.Services.ConfigureService();

var app = builder.Build();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
