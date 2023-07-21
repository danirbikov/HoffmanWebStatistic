using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PingerAPI.Models.General;
using PingerWebAPI.Repository;
using PingerWebAPI.Services;
using Quartz.Impl;
using Quartz;
using ServicesWebAPI.Services;
using System.IO;

namespace PingerWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebServicesController : ControllerBase
    {


        [HttpGet]
        public IResult StartServices()
        {

            Console.WriteLine();

            return null;
        }     
    }
}

