using Microsoft.EntityFrameworkCore;
using Web_API_Task.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<EmployeesAPIdbContext>(options => options.UseInMemoryDatabase("EmployeesDb"));
builder.Services.AddDbContext<EmployeesAPIdbContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();
