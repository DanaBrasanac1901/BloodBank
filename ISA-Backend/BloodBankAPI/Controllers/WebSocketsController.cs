
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using BloodBankAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;
        private readonly HttpClient client;


        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        //Add a new route called ws/
        [HttpGet("/ws")]
        public async Task Get()
        {
            //Check if the current request is via WebSockets otherwise throw a 400.
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }


        private async Task Echo(WebSocket webSocket)
        {
            //singleton baza za trenutnu lokaciju
            StoreLocation storage = StoreLocation.Instance;
            var buffer = new byte[1024 * 4];
            // dobijanje poruke sa fronta
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            
            //Wait until client initiates a request.
            _logger.Log(LogLevel.Information, "Message received from Client");

            string url = "https://localhost:44371/Location";
            //posalji sledecu lokaciju
            await client.GetStringAsync(url);


            //Going into a loop until the client closes the connection
            while (!result.CloseStatus.HasValue)
            {
                for (int i = 1; i <= 14; i++)
                {
                    
                   

                    Thread.Sleep(1000);

                    // await LocationConsumer.Consume();
                    //dobavi lokaciju koja je primljena
                    //meni ovde stalno null iako kad proverim vidim da je zabelezena
                    if (storage.locs.Count!=0)
                    {
                        var loc = storage.locs.Dequeue();
                        //ako je nova posalji je na front
                        var serverMsg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(loc));
                        await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                        _logger.Log(LogLevel.Information, "Message sent to Client");
                        storage.isNew = false;
                        
                    }
                }                
                //Wait until the client send another request.
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message received from Client");
                

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }




    
}