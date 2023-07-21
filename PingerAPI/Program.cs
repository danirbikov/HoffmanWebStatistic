using Microsoft.EntityFrameworkCore;
using PingerAPI;
using PingerAPI.Models;
using PingerAPI.Models.General;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Dictionary<string, bool> standsResult = new Dictionary<string, bool>();


app.MapGet("/StartServices/", (ApplicationDbContext _context) =>
{
    try
    {
        var allStands = _context.stands.Where(k => k.IpAdress != null).ToList();
        Thread pingerThread = new Thread(() => PingAllStandsLoop(allStands));
        pingerThread.Start();


        return Results.Ok("Start");
        
    }

    catch
    {
        return Results.Problem("Services not activated");
    }

    
}
);

app.MapGet("/GetStands/", () =>
{
    return Results.Ok(standsResult);
}
);

app.UseHttpsRedirection();
app.Run();

async Task PingAllStandsLoop(List<Stand> allStands)
{
    foreach (Stand stand in allStands)
    {
        bool connection_status = PingOneStand(stand);
        Console.WriteLine("Stand " + stand.StandName + " bool " + connection_status);
        if (!standsResult.ContainsKey(stand.StandName))
        {
            standsResult.Add(stand.StandName, connection_status);
        }
        else
        {
            standsResult[stand.StandName] = connection_status;
        }
    }

}
bool PingOneStand(Stand stand)
{
    Ping pinger = new Ping();
    bool pingable = false;
    try
    {
        PingReply reply = pinger.Send(stand.IpAdress);
        pingable = reply.Status == IPStatus.Success;

    }
    catch (PingException)
    {
        using (StreamWriter writer = new StreamWriter("Logs/PingerLogs.txt"))
        {
            writer.WriteLine("Exception in pinging stand: ");
            writer.WriteLine(stand.StandName);
            writer.WriteLine(stand.IpAdress);

            writer.WriteLine();
        }
    }
    finally
    {
        if (pinger != null)
        {
            pinger.Dispose();
        }
    }
    return pingable;
}

