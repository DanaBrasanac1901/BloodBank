namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using WebApi.Contracts;

    [ApiController]
    [Route("[controller]")]
    public class LocationController :
        ControllerBase
    {
        readonly IPublishEndpoint _endpoint;
        private LocationRepo _locationRepo { get; set; }

        public LocationController(IPublishEndpoint publishEndpoint)
        {
            _endpoint = publishEndpoint;
            _locationRepo = new LocationRepo();
            _locationRepo.Init();
        }
        [HttpGet]
        public async Task<IActionResult> Send()
        {
            foreach(Location loc in _locationRepo.locations.Values)
            {
                Thread.Sleep(300);
                await _endpoint.Publish<Location>(loc);
            }
            return Ok();
        }
            

            /*foreach(Location loc in locations)
            {
                Thread.Sleep(3000);
                await _endpoint.Publish<Location>(new Location() { Latitude = 34.2434234 });
            }*/

        

    }
}