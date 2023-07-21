using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PingerWebAPI.Repository;
using PingerWebAPI.Services;
using Quartz;
using Quartz.Spi;
using ServicesWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("RunParser");
    q.AddJob<RequestJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("RunParserJobTrigger")
                .StartNow()
                .WithSimpleSchedule(x =>
                        x.WithIntervalInSeconds(60) 
                        .RepeatForever()));
});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);



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
