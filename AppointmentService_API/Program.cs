using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using DataLayer;
 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILogic, Logic>();
builder.Services.AddScoped<IEFRepo, AppointmentEFRepo>();
/*
var trainer_config = builder.Configuration.GetConnectionString("AppointmentService");*/
builder.Services.AddDbContext<AppointmentServiceDbContext>(options => options.UseSqlServer("Server=tcp:teamperseverance.database.windows.net,1433;Initial Catalog=appointmentsdb;Persist Security Info=False;User ID=manoj;Password=Team@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;"));


builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corspolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
