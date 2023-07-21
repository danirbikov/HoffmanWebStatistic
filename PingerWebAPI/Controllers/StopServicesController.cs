using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PingerAPI.Models.General;
using PingerWebAPI.Repository;
using PingerWebAPI.Services;
using Quartz.Impl;
using Quartz;
using ServicesWebAPI.Services;

namespace PingerWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StopServicesController : ControllerBase
    {
       

        private readonly ILogger<WebServicesController> _logger;
        private readonly ApplicationDbContext _dbContext;
        


        public StopServicesController(ILogger<WebServicesController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            
        }

        [HttpGet]
        public IResult StopServices()
        {
           
            

              return Results.Ok("Stopped");
            
        }






    }
}