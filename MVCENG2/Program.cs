using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Quartz;
using NLog;
using NLog.Web;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

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
    builder.Services.AddScoped<JsonPathRepository>();
    builder.Services.AddScoped<Mes2SupPathRepository>();
    builder.Services.AddScoped<Mes2SupTelegramsRepository>();
    builder.Services.AddScoped<Mes2SupTelegramsStandRepository>();
    builder.Services.AddScoped<MesPathRepository>();
    builder.Services.AddScoped<Sup2MesPathRepository>();
    builder.Services.AddScoped<Sup2MesTelegramsRepository>();
    builder.Services.AddScoped<XSDSchemasPurposeRepository>();
    builder.Services.AddScoped<XSDSchemasRepository>();

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
        var jobKey = new JobKey("RunJobServices");
        q.AddJob<RequestJob>(opts => opts.WithIdentity(jobKey));
        q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("RunJobServicesTrigger")
                    .StartNow()
                    .WithSimpleSchedule(x =>
                            x.WithIntervalInSeconds(50)
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
    EmailService emailService = new EmailService();
    //emailService.SendEmail("BikovDI@kamaz.ru", "Webstatistic killed", "������������� ������. Error:\n"+exception.ToString());
    logger.Error(exception, "Stopped program because of exception: " + exception.ToString()) ;
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}