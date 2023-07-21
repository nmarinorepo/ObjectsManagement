using Microsoft.EntityFrameworkCore;
using ObjectsManagement.Application.Implementations;
using ObjectsManagement.Application.Interfaces;
using ObjectsManagement.Application.Repositories;
using ObjectsManagement.Persistence.Context;
using ObjectsManagement.Persistence.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logger configuration section
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ObjectsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IObjectRepository, ObjectRepository>();
builder.Services.AddScoped<IObjectService, ObjectService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=objectmains}/{action=Index}/{id?}");

app.Run();
