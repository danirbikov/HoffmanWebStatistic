using Microsoft.EntityFrameworkCore;
using MVCENG2.ComfortModules;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Services;
using MVCENG2.Repository;
using MVCENG2.Models.Hoffman;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStandRepository, StandRepository>();
builder.Services.AddScoped<IStandsStatisticRepository, StandsStatisticRepository>();
builder.Services.AddScoped<ITestReportRepository, TestReportRepository>();
builder.Services.AddScoped<ITestJsonRepository, TestJsonRepository>();
builder.Services.AddScoped<DateFunctions>();
builder.Services.AddScoped<OperatorsRepository>();
builder.Services.AddScoped<JsonHeadersRepository>();
builder.Services.AddScoped<JsonTestsRepository>();
builder.Services.AddScoped<JsonValuesRepository>();
//builder.Services.AddTransient<ParserJSON>();
builder.Services.AddTransient<Parser>();
builder.Services.AddScoped<Pinger>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    
    
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stands}/{action=Index}/{id?}");

app.Run();
