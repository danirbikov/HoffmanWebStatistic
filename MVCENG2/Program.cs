using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Quartz;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped<StandRepository>();
    builder.Services.AddScoped<OperatorsRepository>();
    builder.Services.AddScoped<JsonHeadersRepository>();
    builder.Services.AddScoped<JsonTestsRepository>();
    builder.Services.AddScoped<JsonValuesRepository>();
    builder.Services.AddScoped<UsersRepository>();
    builder.Services.AddScoped<RolesRepository>();
    builder.Services.AddScoped<PictureRepository>(); 
    builder.Services.AddScoped<TranslateRepository>();
    builder.Services.AddScoped<TranslatePathRepository>();
    builder.Services.AddScoped<SendingStatusLogRepository>();
    builder.Services.AddScoped<PicturePathRepository>();
    builder.Services.AddScoped<OperatorPathRepository>();
    builder.Services.AddScoped<DTCPathRepository>();
    builder.Services.AddScoped<DTCContentRepository>();




    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();
   

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

   

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
                            x.WithIntervalInSeconds(5)
                            .RepeatForever()));
    });

    builder.Services.AddQuartzHostedService(
        q => q.WaitForJobsToComplete = true);



    var app = builder.Build();
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}